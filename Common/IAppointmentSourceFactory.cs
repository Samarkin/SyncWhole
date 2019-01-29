using System.Threading.Tasks;

namespace SyncWhole.Common
{
	public interface IAppointmentSourceFactory
	{
		Task<IAppointmentSource> ConnectSourceAsync();
	}
}