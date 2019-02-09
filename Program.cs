using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SyncWhole.Logging;
using SyncWhole.Properties;
using SyncWhole.UI;

namespace SyncWhole
{
	static class Program
	{
		public static readonly Engine Engine = new Engine();

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var logWindow = new LogWindow();
			Logger.Initialize(logWindow);
			Engine.SetUpSync(
				FactoryStore.CurrentSource,
				FactoryStore.CurrentDestination,
				Settings.Default.SyncInterval);
			using (new TrayIcon(logWindow, new SettingsWindow()))
			{
				Engine.Resume();
				Task _ = Engine.SyncAsync(false);
				Application.Run();
			}
		}
	}
}
