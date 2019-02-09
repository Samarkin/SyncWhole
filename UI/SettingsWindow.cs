using System;
using System.Windows.Forms;
using SyncWhole.Google;
using SyncWhole.Outlook;

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
			Program.Engine.SetUpSync(
				// TODO: Make configurable
				new OutlookAdapterFactory(),
				new GoogleCalendarAdapterFactory("token.json"),
				TimeSpan.FromMinutes((double)_numTimeout.Value));
			Close();
		}

		private async void VisibilityChanged(object sender, EventArgs e)
		{
			if (Visible)
			{
				_btnForce.Enabled = Program.Engine.Ready;
				Program.Engine.Pause();
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
