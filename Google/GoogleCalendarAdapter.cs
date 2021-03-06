﻿using System;
using System.Collections.Async;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using SyncWhole.Common;
using SyncWhole.Logging;

namespace SyncWhole.Google
{
	public sealed class GoogleCalendarAdapter : IAppointmentSource, IAppointmentDestination
	{
		private const string CalendarId = "primary";
		private readonly CalendarService _service;

		public GoogleCalendarAdapter(CalendarService service)
		{
			_service = service;
			Logger.Verbose($"Google Calendar successfully connected");
		}

		public IAsyncEnumerable<ILoadedAppointment> LoadAllAppointments()
		{
			using (Logger.Scope($"GoogleCalendar.LoadAllAppointments()"))
			{
				return new AsyncEnumerable<ILoadedAppointment>(async yield =>
				{
					string pageToken = null;
					do
					{
						EventsResource.ListRequest request = _service.Events.List(CalendarId);
						request.PageToken = pageToken;
						Events events = await request.ExecuteAsync(yield.CancellationToken).ConfigureAwait(false);
						pageToken = events.NextPageToken;
						foreach (Event ev in events.Items)
						{
							if (ev.RecurringEventId == null)
							{
								await yield.ReturnAsync(new GoogleCalendarAppointment(ev)).ConfigureAwait(false);
							}
						}
					} while (pageToken != null);
				});
			}
		}

		public async Task<ILoadedAppointment> FindAppointmentAsync(string uniqueId)
		{
			using (Logger.Scope($"GoogleCalendar.FindAppointment(\"{uniqueId}\")"))
			{
				return await FindAppointmentInternalAsync(uniqueId).ConfigureAwait(false);
			}
		}

		private async Task<GoogleCalendarAppointment> FindAppointmentInternalAsync(string uniqueId)
		{
			EventsResource.ListRequest request = _service.Events.List(CalendarId);
			request.ShowDeleted = true;
			request.ICalUID = uniqueId;
			var events = await request.ExecuteAsync().ConfigureAwait(false);
			Event ev = events.Items.SingleOrDefault(e => e.RecurringEventId == null);
			return ev != null ? new GoogleCalendarAppointment(ev) : null;
		}

		public async Task<ILoadedAppointment> CreateAppointmentAsync(string uniqueId, IAppointment appointmentData)
		{
			using (Logger.Scope($"GoogleCalendar.CreateAppointment(\"{appointmentData}\")"))
			{
				EventsResource.InsertRequest request = _service.Events.Insert(appointmentData.ToGoogleEvent(null, uniqueId), CalendarId);
				Event createdEvent = await request.ExecuteAsync().ConfigureAwait(false);
				Logger.Verbose($"Created new event \"{appointmentData}\" on {appointmentData.Schedule?.Start:d}");
				if (appointmentData.Schedule?.Recurrence?.Exceptions != null)
				{
					await UpdateExceptionsAsync(createdEvent.Id, appointmentData.Schedule, true).ConfigureAwait(false);
				}

				return new GoogleCalendarAppointment(createdEvent);
			}
		}

		private async Task UpdateExceptionsAsync(string id, IAppointmentSchedule schedule, bool force)
		{
			var exceptions = schedule.Recurrence.Exceptions.Where(kv => kv.Value != null);
			foreach (var kv in exceptions)
			{
				using (Logger.Scope($"GoogleCalendar.UpdateException({kv.Key.Date})"))
				{
					EventsResource.InstancesRequest instancesRequest = _service.Events.Instances(CalendarId, id);
					var originalStart = new DateTime(kv.Key.Year, kv.Key.Month, kv.Key.Day,
						schedule.Start.Hour, schedule.Start.Minute, schedule.Start.Second);
					TimeSpan utcOffset = schedule.StartTimeZone.GetUtcOffset(originalStart);
					string offsetFormat = (utcOffset < TimeSpan.Zero ? "\\-" : "") + "hh\\:mm";
					instancesRequest.OriginalStart = $"{originalStart:yyyy-MM-ddTHH:mm:ss}{utcOffset.ToString(offsetFormat)}";
					Events instances = await instancesRequest.ExecuteAsync().ConfigureAwait(false);
					Event instance = instances.Items.FirstOrDefault();
					if (instance == null)
					{
						Logger.Warning($"Failed to find instance of \"{kv.Value}\" at {instancesRequest.OriginalStart} to create an exception");
						continue;
					}

					if (force || kv.Value.LastModifiedDateTime > (instance.Updated ?? DateTime.MinValue))
					{
						await UpdateAppointmentAsync(instance.Id, instance.Sequence, kv.Value, force).ConfigureAwait(false);
					}
				}
			}
		}

		public async Task<ILoadedAppointment> UpdateAppointmentAsync(ILoadedAppointment existingAppointment, IAppointment appointmentData, bool force)
		{
			using (Logger.Scope($"GoogleCalendar.UpdateAppointment(\"{existingAppointment}\")"))
			{
				var googleAppointment = existingAppointment as GoogleCalendarAppointment
					?? throw new ArgumentException("Cannot update appointment from a different calendar");
				return await UpdateAppointmentAsync(googleAppointment.GoogleCalendarEventId, googleAppointment.Sequence, appointmentData, force).ConfigureAwait(false);
			}
		}

		private async Task<ILoadedAppointment> UpdateAppointmentAsync(string id, int? sequence, IAppointment appointmentData, bool force)
		{
			EventsResource.UpdateRequest request = _service.Events.Update(appointmentData.ToGoogleEvent(sequence + 1), CalendarId, id);
			Event updatedEvent = await request.ExecuteAsync().ConfigureAwait(false);
			string description = updatedEvent.RecurringEventId != null
				? "instance of a recurring event"
				: updatedEvent.Recurrence != null
					? "recurring event"
					: "event";
			Logger.Verbose($"Updated {description} \"{appointmentData}\" on {appointmentData.Schedule?.Start:d}");
			if (appointmentData.Schedule?.Recurrence?.Exceptions != null)
			{
				await UpdateExceptionsAsync(updatedEvent.Id, appointmentData.Schedule, force).ConfigureAwait(false);
			}

			return new GoogleCalendarAppointment(updatedEvent);
		}

		public async Task DeleteAppointmentAsync(ILoadedAppointment appointment)
		{
			using (Logger.Scope($"GoogleCalendar.UpdateAppointment(\"{appointment}\")"))
			{
				var googleAppointment = appointment as GoogleCalendarAppointment
					?? throw new ArgumentException("Cannot delete appointment from a different calendar");

				EventsResource.DeleteRequest request = _service.Events.Delete(CalendarId, googleAppointment.GoogleCalendarEventId);
				await request.ExecuteAsync().ConfigureAwait(false);
				Logger.Verbose($"Deleted event \"{appointment}\" on {appointment.Schedule?.Start:d}");
			}
		}

		public void Dispose()
		{
			using (Logger.Scope($"GoogleCalendar.Dispose()"))
			{
				_service.Dispose();
				Logger.Verbose($"Google Calendar disconnected");
			}
		}
	}
}
