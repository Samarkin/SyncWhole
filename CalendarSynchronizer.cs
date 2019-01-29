using System.Collections.Async;
using System.Linq;
using System.Threading.Tasks;
using SyncWhole.Common;

namespace SyncWhole
{
	public sealed class CalendarSynchronizer
	{
		private readonly IAppointmentSourceFactory _sourceFactory;
		private readonly IAppointmentDestinationFactory _destinationFactory;

		public CalendarSynchronizer(IAppointmentSourceFactory sourceFactory, IAppointmentDestinationFactory destinationFactory)
		{
			_sourceFactory = sourceFactory;
			_destinationFactory = destinationFactory;
		}

		public string SourceName => _sourceFactory.ToString();
		public string DestinationName => _destinationFactory.ToString();

		public async Task ClearDestination()
		{
			using (var destination = await _destinationFactory.ConnectDestinationAsync().ConfigureAwait(false))
			{
				await destination
					.LoadAllAppointments()
					.ForEachAsync(destination.DeleteAppointmentAsync)
					.ConfigureAwait(false);
			}
		}

		public async Task Synchronize()
		{
			using (var source = await _sourceFactory.ConnectSourceAsync().ConfigureAwait(false))
			{
				using (var destination = await _destinationFactory.ConnectDestinationAsync().ConfigureAwait(false))
				{
					var sourceAppointments = await source
						.LoadAllAppointments()
						.Take(15)
						.ToArrayAsync()
						.ConfigureAwait(false);

					var destAppointments = await destination
						.LoadAllAppointments()
						.ToArrayAsync()
						.ConfigureAwait(false);

					var newAppointments = sourceAppointments.Except(destAppointments, LoadedAppointmentIdComparer.Default);
					var deletedAppointments = destAppointments.Except(sourceAppointments, LoadedAppointmentIdComparer.Default);

					foreach (var appointment in newAppointments)
					{
						await destination.CreateAppointmentAsync(appointment.UniqueId, appointment).ConfigureAwait(false);
					}

					foreach (var appointment in deletedAppointments)
					{
						await destination.DeleteAppointmentAsync(appointment).ConfigureAwait(false);
					}

					// TODO: Update modified
				}
			}
		}
	}
}
