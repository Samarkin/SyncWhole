using System.Collections.Async;
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

		public Task<ILoadedAppointment> FindAppointmentAsync(string uniqueId)
		{
			throw new System.NotImplementedException();
		}

		public Task<ILoadedAppointment> CreteAppointmentAsync(IAppointment appointment)
		{
			throw new System.NotImplementedException();
		}

		public Task<ILoadedAppointment> UpdateAppointmentAsync(string uniqueId, IAppointment appointment)
		{
			throw new System.NotImplementedException();
		}

		public async Task DeleteAppointmentAsync(ILoadedAppointment appointment)
		{
			EventsResource.DeleteRequest request = _service.Events.Delete(CalendarId, appointment.UniqueId);
			await request.ExecuteAsync().ConfigureAwait(false);
		}

		public void Dispose()
		{
			_service.Dispose();
		}
	}
}
