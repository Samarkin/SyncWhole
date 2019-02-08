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
			GoogleCalendarAppointment existingAppointment = await FindAppointmentInternalAsync(uniqueId);
			if (existingAppointment != null)
			{
				if (!existingAppointment.Deleted)
				{
					throw new InvalidOperationException("Appointment already exists");
				}
				return await UpdateAppointmentAsync(existingAppointment, appointmentData);
			}

			EventsResource.InsertRequest request = _service.Events.Insert(appointmentData.ToGoogleEvent(uniqueId), CalendarId);
			Event createdEvent = await request.ExecuteAsync().ConfigureAwait(false);
			// TODO: Create exceptions
			return new GoogleCalendarAppointment(createdEvent);
		}

		public async Task<ILoadedAppointment> UpdateAppointmentAsync(ILoadedAppointment existingAppointment, IAppointment appointmentData)
		{
			var googleAppointment = existingAppointment as GoogleCalendarAppointment
				?? throw new ArgumentException("Cannot update appointment from a different calendar");

			EventsResource.UpdateRequest request = _service.Events.Update(appointmentData.ToGoogleEvent(), CalendarId, googleAppointment.GoogleCalendarEventId);
			Event updatedEvent = await request.ExecuteAsync().ConfigureAwait(false);
			// TODO: Update exceptions
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
