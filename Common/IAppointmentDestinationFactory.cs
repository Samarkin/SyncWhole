using System.Threading.Tasks;

namespace SyncWhole.Common
{
	public interface IAppointmentDestinationFactory
	{
		Task<IAppointmentDestination> ConnectDestinationAsync();
	}
}