using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;
using SyncWhole.Common;
using SyncWhole.Logging;

namespace SyncWhole.Outlook
{
	public sealed class OutlookAdapterFactory : IAppointmentSourceFactory
	{
		public Task<IAppointmentSource> ConnectSourceAsync()
		{
			using (Logger.Scope($"OutlookCalendar.ConnectSource()"))
			{
				var app = new Application();
				var calendar = app.Session.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);
				return Task.FromResult<IAppointmentSource>(new OutlookAdapter(calendar));
			}
		}

		public override string ToString() => "Outlook Application";
	}
}