using System;
using System.Collections.Async;
using System.Windows.Forms;
using SyncWhole.Common;
using SyncWhole.Google;
using SyncWhole.Outlook;

namespace SyncWhole
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			var googleFactory = new GoogleCalendarAdapterFactory("token.json");
			var outlookFactory = new OutlookAdapterFactory();

			_cmbCalendars.Items.Add(googleFactory);
			_cmbCalendars.Items.Add(outlookFactory);
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
	}
}
