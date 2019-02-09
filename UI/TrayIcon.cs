using System;
using System.Reflection;
using System.Windows.Forms;

namespace SyncWhole.UI
{
	public sealed class TrayIcon : IDisposable
	{
		private readonly NotifyIcon _icon;

		public TrayIcon(LogWindow logWindow, SettingsWindow settingsWindow)
		{
			var syncMenuItem = new MenuItem("Synchronize now!", SyncClick);
			var nextSyncMenuItem = new MenuItem("-") {Enabled = false};
			_icon = new NotifyIcon
			{
				Icon = Properties.Resources.tray_icon,
				ContextMenu = new ContextMenu(new[]
				{
					nextSyncMenuItem,
					syncMenuItem,
					new MenuItem("-"),
					new MenuItem("Show &log", (o, e) => logWindow.Display()),
					new MenuItem("&Settings", (o,e) => settingsWindow.Display()),
					new MenuItem("E&xit", (o, e) => Application.Exit()),
				}),
				Text = $"SyncWhole v{Assembly.GetExecutingAssembly().GetName().Version}",
				Visible = true,
			};
			_icon.ContextMenu.Popup += (o, e) =>
			{
				var nextSync = Program.Engine.NextSync;
				nextSyncMenuItem.Text = nextSync.HasValue
					? $"Next sync: {nextSync.Value:G}"
					: Program.Engine.SyncInProcess
						? "Synchronizing..."
						: "Auto-sync disabled";
				syncMenuItem.Enabled = Program.Engine.Ready;
			};
		}

		private static async void SyncClick(object sender, EventArgs args)
		{
			await Program.Engine.SyncAsync(false);
		}

		public void Dispose()
		{
			_icon.Dispose();
		}
	}
}