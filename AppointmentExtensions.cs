using System.Text;
using SyncWhole.Common;

namespace SyncWhole
{
	internal static class AppointmentExtensions
	{
		public static string ToLongString(this ILoadedAppointment appointment)
		{
			var sb = new StringBuilder();
			sb.AppendLine(appointment.UniqueId);
			sb.AppendLine();
			sb.AppendLine(appointment.Busy ? "Busy" : "Free");
			if (!appointment.Confirmed)
			{
				sb.AppendLine("Tentative");
			}
			sb.AppendLine(appointment.Subject);
			sb.AppendLine(appointment.Location);

			sb.AppendLine();
			if (appointment.Schedule == null)
			{
				sb.AppendLine("No schedule");
			}
			else if (appointment.Schedule.AllDay && appointment.Schedule.Start.Date.AddDays(1) == appointment.Schedule.End.Date)
			{
				sb.AppendLine($"{appointment.Schedule.Start.ToShortDateString()}");
			}
			else if (appointment.Schedule.AllDay)
			{
				sb.AppendLine($"{appointment.Schedule.Start.ToShortDateString()} - {appointment.Schedule.End.AddDays(-1).ToShortDateString()}");
			}
			else if (appointment.Schedule.Start.Date == appointment.Schedule.End.Date)
			{
				sb.AppendLine($"{appointment.Schedule.Start} - {appointment.Schedule.End.ToShortTimeString()}");
			}
			else
			{
				sb.AppendLine($"{appointment.Schedule.Start} - {appointment.Schedule.End}");
			}

			if (appointment.Schedule?.Recurrence != null)
			{
				sb.AppendLine();
				sb.AppendLine($"{appointment.Schedule.Recurrence.Frequency} recurrence!");
			}

			return sb.ToString();
		}
	}
}