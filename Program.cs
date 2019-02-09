using System;
using System.Windows.Forms;
using SyncWhole.Logging;
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
			using (new TrayIcon(logWindow, new SettingsWindow()))
			{
				Application.Run();
			}
		}
	}
}
