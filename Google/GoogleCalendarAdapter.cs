using System;
using System.Collections.Async;
using System.Diagnostics;
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
			request.ICalUID = uniqueId;
			var events = await request.ExecuteAsync().ConfigureAwait(false);
			// TODO: Repetitions return multiple events
			return new GoogleCalendarAppointment(events.Items.Single());
		}

		public async Task<ILoadedAppointment> CreateAppointmentAsync(string uniqueId, IAppointment appointment)
		{
			EventsResource.InsertRequest request = _service.Events.Insert(Convert(uniqueId, appointment), CalendarId);
			Event createdEvent = await request.ExecuteAsync().ConfigureAwait(false);
			return new GoogleCalendarAppointment(createdEvent);
		}

		private static Event Convert(string uniqueId, IAppointment appointment)
		{
			return new Event
			{
				Transparency = appointment.Busy ? "opaque" : "transparent",
				Summary = appointment.Subject,
				Location = appointment.Location,
				ICalUID = uniqueId,
				Start = new EventDateTime {DateTime = appointment.Schedule.Start},
				End = new EventDateTime {DateTime = appointment.Schedule.End},
			};
		}

		public Task<ILoadedAppointment> UpdateAppointmentAsync(string uniqueId, IAppointment appointment)
		{
			throw new System.NotImplementedException();
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
