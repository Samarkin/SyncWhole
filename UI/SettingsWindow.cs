using System;
using System.Windows.Forms;
using SyncWhole.Common;
using SyncWhole.Properties;

namespace SyncWhole.UI
{
	public partial class SettingsWindow : FormBase
	{
		private bool _startForceSync;

		public SettingsWindow()
		{
			InitializeComponent();
		}

		private void CancelClick(object sender, EventArgs e)
		{
			Close();
		}

		private void OkClick(object sender, EventArgs e)
		{
			var interval = TimeSpan.FromMinutes((int)_numTimeout.Value);
			Program.Engine.SetUpSync(
				(IAppointmentSourceFactory)_cmbSource.SelectedItem,
				(IAppointmentDestinationFactory)_cmbDestination.SelectedItem,
				interval);
			Settings.Default.SyncInterval = interval;
			Settings.Default.Save();
			Close();
		}

		private async void VisibilityChanged(object sender, EventArgs e)
		{
			if (Visible)
			{
				_btnForce.Enabled = Program.Engine.Ready;
				Program.Engine.Pause();
				_cmbSource.Items.Clear();
				_cmbSource.Items.Add(FactoryStore.CurrentSource);
				_cmbSource.SelectedIndex = 0;
				_cmbSource.Enabled = false;
				_cmbDestination.Items.Clear();
				_cmbDestination.Items.Add(FactoryStore.CurrentDestination);
				_cmbDestination.SelectedIndex = 0;
				_cmbDestination.Enabled = false;
				_numTimeout.Value = (decimal)Settings.Default.SyncInterval.TotalMinutes;
			}
			else
			{
				Program.Engine.Resume();
				if (_startForceSync)
				{
					_startForceSync = false;
					await Program.Engine.SyncAsync(true);
				}
			}
		}

		private void ForceClick(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show(
				$"Are you sure you want to perform full synchronization?{Environment.NewLine}"
				+ "It will take longer than the scheduled one, but should be able to fix some synchronization problems",
				"Full synchronization",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button2);
			if (result == DialogResult.Yes)
			{
				_startForceSync = true;
				Close();
			}
		}
	}
}
