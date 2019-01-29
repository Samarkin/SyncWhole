using System;
using System.Collections.Async;

namespace SyncWhole.Common
{
	public interface IAppointmentSource : IDisposable
	{
		IAsyncEnumerable<ILoadedAppointment> LoadAllAppointments();
	}
}