using System;
using System.Collections.Async;
using System.Threading.Tasks;

namespace SyncWhole.Common
{
	public interface IAppointmentDestination : IDisposable
	{
		IAsyncEnumerable<ILoadedAppointment> LoadAllAppointments();

		Task<ILoadedAppointment> FindAppointmentAsync(string uniqueId);
		Task<ILoadedAppointment> CreateAppointmentAsync(string uniqueId, IAppointment appointment);
		Task<ILoadedAppointment> UpdateAppointmentAsync(string uniqueId, IAppointment appointment);
		Task DeleteAppointmentAsync(ILoadedAppointment appointment);
	}
}