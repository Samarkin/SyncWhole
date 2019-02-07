using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;
using SyncWhole.Common;

namespace SyncWhole.Google
{
	internal static class AppointmentExtensions
	{
		private static readonly KeyValuePair<WeekDay, string>[] WeekDayMap =
		{
			new KeyValuePair<WeekDay,string>(WeekDay.Sunday, "SU"),
			new KeyValuePair<WeekDay,string>(WeekDay.Monday, "MO"),
			new KeyValuePair<WeekDay,string>(WeekDay.Tuesday, "TU"),
			new KeyValuePair<WeekDay,string>(WeekDay.Wednesday, "WE"),
			new KeyValuePair<WeekDay,string>(WeekDay.Thursday, "TH"),
			new KeyValuePair<WeekDay,string>(WeekDay.Friday, "FR"),
			new KeyValuePair<WeekDay,string>(WeekDay.Saturday, "SA"),
		};

		private static readonly Dictionary<string, string> OutlookToGoogleTimeZoneMap = new Dictionary<string, string>
		{
			{"(UTC-08:00) Pacific Time (US & Canada)", "America/Los_Angeles"},
			// TODO: More timezones
		};

		public static Event ToGoogleEvent(this IAppointment appointment, string uniqueId = null)
		{
			if (appointment.Schedule == null)
			{
				throw new ArgumentException("Appointment does not contain schedule");
			}

			return new Event
			{
				Transparency = appointment.Busy ? "opaque" : "transparent",
				Summary = appointment.Subject,
				Location = appointment.Location,
				ICalUID = uniqueId,
				Status = appointment.Confirmed ? "confirmed" : "tentative",
				Start = new EventDateTime
				{
					DateTime = appointment.Schedule.Start,
					TimeZone = ToGoogleTimezone(appointment.Schedule.StartTimeZone),
				},
				End = new EventDateTime
				{
					DateTime = appointment.Schedule.End,
					TimeZone = ToGoogleTimezone(appointment.Schedule.EndTimeZone),
				},
				Recurrence =  appointment.Schedule.ToRfc5545Rules().ToList(),
			};
		}

		public static IEnumerable<string> ToRfc5545Rules(this IAppointmentSchedule schedule)
		{
			var recurrence = schedule.Recurrence;
			if (recurrence == null)
			{
				yield break;
			}

			var sb = new StringBuilder();
			sb.Append("RRULE:FREQ=");
			switch (recurrence.Frequency)
			{
				case RecurrenceFrequency.Daily:
					sb.Append("DAILY");
					break;
				case RecurrenceFrequency.Weekly:
					sb.Append("WEEKLY");
					break;
				case RecurrenceFrequency.Monthly:
					sb.Append("MONTHLY");
					break;
				case RecurrenceFrequency.Yearly:
					sb.Append("YEARLY");
					break;
			}
			if (recurrence.Count.HasValue)
			{
				sb.Append($";COUNT={recurrence.Count.Value}");
			}
			if (recurrence.Interval.HasValue)
			{
				sb.Append($";INTERVAL={recurrence.Interval.Value}");
			}
			if (recurrence.Until.HasValue)
			{
				sb.Append($";UNTIL={recurrence.Until.Value.Date:yyyyMMddT000000Z}"); // TODO: is UTC okay?
			}
			if (recurrence.WeekDay.HasValue)
			{
				sb.Append($";BYDAY={ToRfc5545String(recurrence.WeekDay.Value)}");
			}
			if (recurrence.YearMonth.HasValue)
			{
				sb.Append($";BYMONTH={recurrence.YearMonth.Value}");
			}
			if (recurrence.MonthDay.HasValue)
			{
				sb.Append($";BYMONTHDAY={recurrence.MonthDay.Value}");
			}
			if (recurrence.Exceptions != null && recurrence.Exceptions.Length > 0)
			{
				string startTime = schedule.Start.ToString("HHmmss");
				string startTimezone = ToGoogleTimezone(schedule.StartTimeZone);
				foreach (DateTime exceptionDate in recurrence.Exceptions)
				{
					yield return $"EXDATE;TZID={startTimezone}:{exceptionDate:yyyyMMdd}T{startTime}";
				}
			}

			yield return sb.ToString();
		}

		private static string ToRfc5545String(WeekDay weekDay)
		{
			return string.Join(",", WeekDayMap.Where(kv => weekDay.HasFlag(kv.Key)).Select(kv => kv.Value));
		}

		private static string ToGoogleTimezone(string outlookTimezone)
		{
			if (!OutlookToGoogleTimeZoneMap.TryGetValue(outlookTimezone, out string timezone))
			{
				throw new ArgumentException($"Unknown timezone: \"{outlookTimezone}\"", nameof(outlookTimezone));
			}
			return timezone;
		}
	}
}
