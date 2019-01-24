using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace SyncWhole
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private async void ButtonClick(object sender, EventArgs e)
		{
			_btnLoad.Enabled = false;
			try
			{
				listBox1.Items.Clear();
				await Task.Run(() => LoadAppointments(vm => BeginInvoke((Action)(() => listBox1.Items.Add(vm)))));
			}
			finally
			{
				_btnLoad.Enabled = true;
			}
		}

		private static void LoadAppointments(Action<AppointmentViewModel> callback)
		{
			var app = new Outlook.Application();
			var calendar = app.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderCalendar);

			foreach (Outlook.AppointmentItem item in calendar.Items)
			{
				callback(new AppointmentViewModel(item));
			}
		}


		private class AppointmentViewModel
		{
			private readonly Outlook.AppointmentItem _appointment;

			public AppointmentViewModel(Outlook.AppointmentItem appointment)
			{
				_appointment = appointment;
			}

			public override string ToString()
			{
				return _appointment.Subject ?? "<null>";
			}

			private static string ToString(Outlook.OlDaysOfWeek daysOfWeek)
			{
				KeyValuePair<Outlook.OlDaysOfWeek, string>[] arr =
				{
					new KeyValuePair<Outlook.OlDaysOfWeek, string> (Outlook.OlDaysOfWeek.olMonday, "Monday"),
					new KeyValuePair<Outlook.OlDaysOfWeek, string> (Outlook.OlDaysOfWeek.olTuesday, "Tuesday"),
					new KeyValuePair<Outlook.OlDaysOfWeek, string> (Outlook.OlDaysOfWeek.olWednesday, "Wednesday"),
					new KeyValuePair<Outlook.OlDaysOfWeek, string> (Outlook.OlDaysOfWeek.olThursday, "Thursday"),
					new KeyValuePair<Outlook.OlDaysOfWeek, string> (Outlook.OlDaysOfWeek.olFriday, "Friday"),
					new KeyValuePair<Outlook.OlDaysOfWeek, string> (Outlook.OlDaysOfWeek.olSaturday, "Saturday"),
					new KeyValuePair<Outlook.OlDaysOfWeek, string> (Outlook.OlDaysOfWeek.olSunday, "Sunday"),
				};

				return string.Join(", ", arr.Where(kv => daysOfWeek.HasFlag(kv.Key)).Select(kv => kv.Value));
			}

			public string ToLongString()
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine(_appointment.BusyStatus.ToString());
				sb.AppendLine(_appointment.Subject);
				sb.AppendLine(_appointment.Location);
				if (_appointment.AllDayEvent)
				{
					sb.AppendLine($"{_appointment.Start.ToShortDateString()}");
				}
				else if (_appointment.Start.Date == _appointment.End.Date)
				{
					sb.AppendLine($"{_appointment.Start} - {_appointment.End.ToShortTimeString()}");
				}
				else
				{
					sb.AppendLine($"{_appointment.Start} - {_appointment.End}");
				}

				if (_appointment.IsRecurring)
				{
					var pattern = _appointment.GetRecurrencePattern();
					switch (pattern.RecurrenceType)
					{
						case Outlook.OlRecurrenceType.olRecursDaily:
							sb.Append($"Repeat daily every {pattern.Interval} days");
							break;
						case Outlook.OlRecurrenceType.olRecursWeekly:
							sb.Append($"Repeat weekly every {pattern.Interval} week on {ToString(pattern.DayOfWeekMask)}");
							break;
						case Outlook.OlRecurrenceType.olRecursMonthly:
							sb.Append($"Repeat monthly every {pattern.Interval} month on the {pattern.DayOfMonth} day of the month");
							break;
						case Outlook.OlRecurrenceType.olRecursYearly:
							sb.Append($"Repeat yearly on the {pattern.DayOfMonth} day of the {pattern.MonthOfYear} month");
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
					sb.AppendLine($" until {(pattern.NoEndDate ? "pigs fly" : pattern.PatternEndDate.ToShortDateString())}");

					foreach (Outlook.Exception exception in pattern.Exceptions)
					{
						if (exception.Deleted)
						{
							sb.AppendLine($"  except on {exception.OriginalDate.ToShortDateString()}");
						}
						else if (exception.AppointmentItem.Start.ToShortTimeString() != _appointment.Start.ToShortTimeString()
							|| exception.AppointmentItem.End.ToShortTimeString() != _appointment.End.ToShortTimeString())
						{
							sb.AppendLine($"  but on {exception.OriginalDate.ToShortDateString()} @ {exception.AppointmentItem.Start.ToShortTimeString()} - {exception.AppointmentItem.End.ToShortTimeString()}");
						}
						else if (exception.AppointmentItem.Subject != _appointment.Subject)
						{
							sb.AppendLine($"  but on {exception.OriginalDate.ToShortDateString()} - {exception.AppointmentItem.Subject}");
						}
						else if (exception.AppointmentItem.Location != _appointment.Location)
						{
							sb.AppendLine($"  but on {exception.OriginalDate.ToShortDateString()} @ {exception.AppointmentItem.Location}");
						}
						else
						{
							sb.AppendLine($"  but on {exception.OriginalDate.ToShortDateString()} it is different somehow");
						}
					}
				}

				sb.AppendLine();
				sb.AppendLine(_appointment.GlobalAppointmentID);

				return sb.ToString();
			}
		}

		private void ListBoxSelectionChanged(object sender, EventArgs e)
		{
			var apt = listBox1.SelectedItem as AppointmentViewModel;
			if (apt == null)
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
