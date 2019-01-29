using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;
using SyncWhole.Common;

namespace SyncWhole.Outlook
{
	public sealed class OutlookAdapterFactory : IAppointmentSourceFactory
	{
		public Task<IAppointmentSource> ConnectSourceAsync()
		{
			var app = new Application();
			var calendar = app.Session.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);
			return Task.FromResult<IAppointmentSource>(new OutlookAdapter(calendar));
		}
	}
}