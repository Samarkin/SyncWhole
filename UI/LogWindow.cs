using System;
using System.Globalization;
using System.Windows.Forms;
using SyncWhole.Logging;
using SyncWhole.Properties;

namespace SyncWhole.UI
{
	public partial class LogWindow : FormBase, ILogWriter
	{
		private const int MaxItemsInTheList = 10000;
		private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";

		public LogLevel MaxLevel => Settings.Default.LogLevel;

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

		private void AddItemToTheList(ListViewItem item)
		{
			Dispatch(() =>
			{
				if (_lstLog.Items.Count == MaxItemsInTheList)
				{
					_lstLog.Items.RemoveAt(0);
				}
				_lstLog.Items.Add(item);
			});
		}

		public void WriteMessage(LogLevel level, string message)
		{
			if (level > MaxLevel)
			{
				return;
			}

			DateTime dateTime = DateTime.Now;
			AddItemToTheList(new ListViewItem(new []
			{
				string.Empty,
				dateTime.ToString(DateTimeFormat, CultureInfo.InvariantCulture),
				message,
			}, GetImageKey(level)));
		}

		public void WriteMessage(Exception exception, string message)
		{
			if (MaxLevel < LogLevel.Error)
			{
				return;
			}

			DateTime dateTime = DateTime.Now;
			AddItemToTheList(new ListViewItem(new []
			{
				string.Empty,
				dateTime.ToString(DateTimeFormat, CultureInfo.InvariantCulture),
				$"{message}: {exception.Message}",
			}, GetImageKey(LogLevel.Error))
			{
				ToolTipText = exception.ToString()
			});
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
				case LogLevel.Verbose:
					return "verbose";
				default:
					return string.Empty;
			}
		}
	}
}
