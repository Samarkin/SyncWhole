using System.Collections.Async;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;
using SyncWhole.Common;

namespace SyncWhole.Outlook
{
	public sealed class OutlookAdapter : IAppointmentSource
	{
		private readonly MAPIFolder _calendarFolder;

		public OutlookAdapter(MAPIFolder calendarFolder)
		{
			_calendarFolder = calendarFolder;
		}

		public IAsyncEnumerable<ILoadedAppointment> LoadAllAppointments()
		{
			return new AsyncEnumerable<ILoadedAppointment>(yield => Task.Run(async () =>
			{
				foreach (AppointmentItem item in _calendarFolder.Items)
				{
					yield.CancellationToken.ThrowIfCancellationRequested();
					await yield.ReturnAsync(new OutlookAppointment(item)).ConfigureAwait(false);
				}
			}));
		}

		public void Dispose()
		{
		}
	}
}
