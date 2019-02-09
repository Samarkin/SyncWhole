using System;
using System.Threading;
using System.Threading.Tasks;
using SyncWhole.Common;
using SyncWhole.Logging;

namespace SyncWhole
{
	public sealed class Engine
	{
		private readonly object _stateLock = new object();
		private readonly Timer _timer;
		private TimeSpan _timerInterval = TimeSpan.FromMinutes(15);
		private bool _paused;

		private CalendarSynchronizer _synchronizer;
		public bool SyncInProcess { get; private set; }

		public bool Ready => _synchronizer != null && !SyncInProcess && !_paused;
		public DateTime? NextSync { get; private set; }

		public Engine()
		{
			_timer = new Timer(OnTimer);
			_paused = true;
		}

		public void SetUpSync(IAppointmentSourceFactory source, IAppointmentDestinationFactory destination, TimeSpan interval)
		{
			lock (_stateLock)
			{
				if (!_paused)
				{
					throw new InvalidOperationException("Cannot set up until paused");
				}
				_synchronizer = new CalendarSynchronizer(source, destination);
				_timerInterval = interval;
				Logger.Info($"Scheduled synchronization from {_synchronizer.SourceName} to {_synchronizer.DestinationName} every {interval}");
			}
		}

		public void Pause()
		{
			lock (_stateLock)
			{
				_paused = true;
				RewindTimer();
				Logger.Info("Synchronization paused");
			}
		}

		public void Resume()
		{
			lock (_stateLock)
			{
				if (!_paused)
				{
					return;
				}
				_paused = false;
				if (Ready)
				{
					RewindTimer();
					Logger.Info("Synchronization resumed");
				}
			}
		}

		private void RewindTimer()
		{
			_timer.Change(_paused ? Timeout.InfiniteTimeSpan : _timerInterval, Timeout.InfiniteTimeSpan);
			NextSync = _paused ? (DateTime?)null : DateTime.Now.Add(_timerInterval);
		}

		private async void OnTimer(object state)
		{
			lock (_stateLock)
			{
				if (!Ready)
				{
					RewindTimer();
					return;
				}
				NextSync = null;
			}
			await SyncAsync(false).ConfigureAwait(false);
			lock (_stateLock)
			{
				RewindTimer();
			}
		}

		public async Task SyncAsync(bool force)
		{
			if (!Ready)
			{
				return;
			}

			try
			{
				SyncInProcess = true;
				var statistics = await _synchronizer.SynchronizeAsync(force).ConfigureAwait(false);
				Logger.Info($"Synchronization successful. {statistics.Created} new events created, {statistics.Deleted} old events deleted, {statistics.Updated} events updated");
			}
			catch (Exception ex)
			{
				Logger.Error("Synchronization failed", ex);
			}
			finally
			{
				SyncInProcess = false;
			}
		}
	}
}