using System;
using System.Collections.Async;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SyncWhole.Common;

namespace SyncWhole
{
	public sealed class CalendarSynchronizer
	{
		private readonly IAppointmentSourceFactory _sourceFactory;
		private readonly IAppointmentDestinationFactory _destinationFactory;

		public CalendarSynchronizer(IAppointmentSourceFactory sourceFactory,
			IAppointmentDestinationFactory destinationFactory)
		{
			_sourceFactory = sourceFactory;
			_destinationFactory = destinationFactory;
		}

		public string SourceName => _sourceFactory.ToString();
		public string DestinationName => _destinationFactory.ToString();

		public async Task<Statistics> Synchronize(bool force)
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
					var modifiedAppointments = sourceAppointments.SmartZip(destAppointments, LoadedAppointmentIdComparer.Default,
						(src, dest) => new { Source = src, Destination = dest});

					if (!force)
					{
						modifiedAppointments = modifiedAppointments.Where(pair => pair.Source.LastModifiedDateTime > pair.Destination.LastModifiedDateTime);
					}

					int created = 0, deleted = 0, updated = 0;

					foreach (var appointment in newAppointments)
					{
						await destination
							.CreateAppointmentAsync(appointment.UniqueId, appointment)
							.ConfigureAwait(false);
						created++;
					}

					foreach (var appointment in deletedAppointments)
					{
						await destination
							.DeleteAppointmentAsync(appointment)
							.ConfigureAwait(false);
						deleted++;
					}

					foreach (var pair in modifiedAppointments)
					{
						await destination
							.UpdateAppointmentAsync(pair.Destination, pair.Source)
							.ConfigureAwait(false);
						updated++;
					}

					return new Statistics(created, deleted, updated);
				}
			}
		}

		public struct Statistics
		{
			public int Created { get; }
			public int Deleted { get; }
			public int Updated { get; }

			public Statistics(int created, int deleted, int updated)
			{
				Created = created;
				Deleted = deleted;
				Updated = updated;
			}

		}

	}
}
