using System;
using System.Collections.Async;
using System.Windows.Forms;
using SyncWhole.Common;
using SyncWhole.Google;
using SyncWhole.Logging;
using SyncWhole.Outlook;
using SyncWhole.UI;

namespace SyncWhole
{
	public partial class Form1 : Form
	{
		private readonly CalendarSynchronizer _synchronizer;
		private readonly LogWindow _logWindow;

		public Form1()
		{
			InitializeComponent();

			var googleFactory = new GoogleCalendarAdapterFactory("token.json");
			var outlookFactory = new OutlookAdapterFactory();
			_synchronizer = new CalendarSynchronizer(outlookFactory, googleFactory);

			_cmbCalendars.Items.Add(googleFactory);
			_cmbCalendars.Items.Add(outlookFactory);

			_logWindow = new LogWindow();
			Logger.Initialize(_logWindow);
		}

		private async void ButtonClick(object sender, EventArgs e)
		{
			_btnLoad.Enabled = false;
			try
			{
				listBox1.Items.Clear();
				if (!(_cmbCalendars.SelectedItem is IAppointmentSourceFactory factory))
				{
					return;
				}
				using (IAppointmentSource source = await factory.ConnectSourceAsync())
				{
					await source.LoadAllAppointments()
						.ForEachAsync(a => BeginInvoke((Action) (() => listBox1.Items.Add(a))));
				}
			}
			finally
			{
				_btnLoad.Enabled = true;
			}
		}

		private async void SyncClick(object sender, EventArgs e)
		{
			_btnSync.Enabled = false;
			_chkForce.Enabled = false;
			try
			{
				var statistics = await _synchronizer.Synchronize(_chkForce.Checked);
				MessageBox.Show(
					this,
					$"{statistics.Created} new events created\r\n{statistics.Deleted} old events deleted\r\n{statistics.Updated} events updated",
					"Synchronization successful");
			}
			finally
			{
				_btnSync.Enabled = true;
				_chkForce.Enabled = true;
			}
		}

		private void ListBoxSelectionChanged(object sender, EventArgs e)
		{
			if (!(listBox1.SelectedItem is ILoadedAppointment apt))
			{
				return;
			}

			_lblDetails.Text = apt.ToLongString();
		}

		private void Form1Closing(object sender, FormClosingEventArgs e)
		{
		}

		private void LogClick(object sender, EventArgs e)
		{
			_logWindow.Display();
		}
	}
}
