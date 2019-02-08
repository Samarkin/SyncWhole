using System;
using System.Collections.Async;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using SyncWhole.Common;

namespace SyncWhole.Google
{
	public sealed class GoogleCalendarAdapter : IAppointmentSource, IAppointmentDestination
	{
		private const string CalendarId = "primary";
		private readonly CalendarService _service;

		public GoogleCalendarAdapter(CalendarService service)
		{
			_service = service;
		}

		public IAsyncEnumerable<ILoadedAppointment> LoadAllAppointments()
		{
			return new AsyncEnumerable<ILoadedAppointment>(async yield =>
			{
				EventsResource.ListRequest request = _service.Events.List(CalendarId);
				Events events = await request.ExecuteAsync(yield.CancellationToken).ConfigureAwait(false);
				foreach (Event ev in events.Items)
				{
					await yield.ReturnAsync(new GoogleCalendarAppointment(ev)).ConfigureAwait(false);
				}
			});
		}

		public async Task<ILoadedAppointment> FindAppointmentAsync(string uniqueId)
		{
			return await FindAppointmentInternalAsync(uniqueId);
		}

		public async Task<GoogleCalendarAppointment> FindAppointmentInternalAsync(string uniqueId)
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
			EventsResource.InsertRequest request = _service.Events.Insert(appointmentData.ToGoogleEvent(uniqueId), CalendarId);
			Event createdEvent = await request.ExecuteAsync().ConfigureAwait(false);
			if (appointmentData.Schedule?.Recurrence?.Exceptions != null)
			{
				await UpdateExceptionsAsync(createdEvent.Id, appointmentData.Schedule).ConfigureAwait(false);
			}

			return new GoogleCalendarAppointment(createdEvent);
		}

		private async Task UpdateExceptionsAsync(string id, IAppointmentSchedule schedule)
		{
			var exceptions = schedule.Recurrence.Exceptions.Where(kv => kv.Value != null).ToArray();
			foreach (var kv in exceptions)
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
					throw new InvalidOperationException("Cannot cancel instance");
				}

				// TODO: compare modification time before updating for optimization
				await UpdateAppointmentAsync(instance.Id, kv.Value).ConfigureAwait(false);
			}
		}

		public Task<ILoadedAppointment> UpdateAppointmentAsync(ILoadedAppointment existingAppointment, IAppointment appointmentData)
		{
			var googleAppointment = existingAppointment as GoogleCalendarAppointment
				?? throw new ArgumentException("Cannot update appointment from a different calendar");
			return UpdateAppointmentAsync(googleAppointment.GoogleCalendarEventId, appointmentData);
		}

		public async Task<ILoadedAppointment> UpdateAppointmentAsync(string id, IAppointment appointmentData)
		{
			EventsResource.UpdateRequest request = _service.Events.Update(appointmentData.ToGoogleEvent(), CalendarId, id);
			Event updatedEvent = await request.ExecuteAsync().ConfigureAwait(false);
			if (appointmentData.Schedule?.Recurrence?.Exceptions != null)
			{
				await UpdateExceptionsAsync(updatedEvent.Id, appointmentData.Schedule).ConfigureAwait(false);
			}

			return new GoogleCalendarAppointment(updatedEvent);
		}

		public async Task DeleteAppointmentAsync(ILoadedAppointment appointment)
		{
			var googleAppointment = appointment as GoogleCalendarAppointment
				?? throw new ArgumentException("Cannot delete appointment from a different calendar");

			EventsResource.DeleteRequest request = _service.Events.Delete(CalendarId, googleAppointment.GoogleCalendarEventId);
			await request.ExecuteAsync().ConfigureAwait(false);
		}

		public void Dispose()
		{
			_service.Dispose();
		}
	}
}
