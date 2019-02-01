using System;
using System.Collections.Async;
using System.Threading.Tasks;

namespace SyncWhole.Common
{
	public interface IAppointmentDestination : IDisposable
	{
		IAsyncEnumerable<ILoadedAppointment> LoadAllAppointments();

		Task<ILoadedAppointment> FindAppointmentAsync(string uniqueId);
		Task<ILoadedAppointment> CreateAppointmentAsync(string uniqueId, IAppointment appointmentData);
		Task<ILoadedAppointment> UpdateAppointmentAsync(ILoadedAppointment existingAppointment, IAppointment appointmentData);
		Task DeleteAppointmentAsync(ILoadedAppointment appointment);
	}
}