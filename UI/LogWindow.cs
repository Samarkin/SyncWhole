using System;
using System.Globalization;
using System.Windows.Forms;
using SyncWhole.Logging;

namespace SyncWhole.UI
{
	public partial class LogWindow : FormBase, ILogWriter
	{
		private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";

		// TODO: Make configurable through UI
		public LogLevel MaxLevel => LogLevel.Info;

		public LogWindow()
		{
			InitializeComponent();
		}

		private void CloseClick(object sender, EventArgs e)
		{
			Close();
		}

		private void ClearClick(object sender, EventArgs e)
		{
			_lstLog.Items.Clear();
		}

		public void WriteMessage(LogLevel level, string message)
		{
			if (level > MaxLevel)
			{
				return;
			}

			DateTime dateTime = DateTime.Now;
			Dispatch(() => _lstLog.Items.Add(new ListViewItem(new []
			{
				string.Empty,
				dateTime.ToString(DateTimeFormat, CultureInfo.InvariantCulture),
				message,
			}, GetImageKey(level))));
		}

		public void WriteMessage(Exception exception, string message)
		{
			if (MaxLevel < LogLevel.Error)
			{
				return;
			}

			DateTime dateTime = DateTime.Now;
			Dispatch(() => _lstLog.Items.Add(new ListViewItem(new []
			{
				string.Empty,
				dateTime.ToString(DateTimeFormat, CultureInfo.InvariantCulture),
				$"{message}: {exception.Message}",
			}, GetImageKey(LogLevel.Error))
			{
				ToolTipText = exception.ToString()
			}));
		}

		private static string GetImageKey(LogLevel level)
		{
			switch (level)
			{
				case LogLevel.Error:
					return "error";
				case LogLevel.Warning:
					return "warning";
				case LogLevel.Info:
					return "info";
				case LogLevel.Debug:
					return "debug";
				default:
					return string.Empty;
			}
		}
	}
}
