using System.Text;
using SyncWhole.Common;

namespace SyncWhole
{
	internal static class AppointmentExtensions
	{
		public static string ToLongString(this ILoadedAppointment appointment)
		{
			var sb = new StringBuilder();
			sb.AppendLine(appointment.Subject);
			sb.AppendLine(appointment.Location);
			return sb.ToString();
		}
	}
}