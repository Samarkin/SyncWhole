using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Calendar.v3.Data;
using SyncWhole.Common;
using TimeZoneConverter;

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

		public static Event ToGoogleEvent(this IAppointment appointment, string uniqueId = null)
		{
			if (appointment.Schedule == null)
			{
				throw new ArgumentException("Appointment does not contain schedule");
			}

			var ev = new Event
			{
				Transparency = appointment.Busy ? "opaque" : "transparent",
				Summary = appointment.Subject,
				Location = appointment.Location,
				ICalUID = uniqueId,
				Status = appointment.Confirmed ? "confirmed" : "tentative",
				Reminders = new Event.RemindersData
				{
					UseDefault = false,
				},
				Recurrence =  appointment.Schedule.ToRfc5545Rules().ToList(),
			};
			if (appointment.Schedule.AllDay)
			{
				ev.Start = new EventDateTime
				{
					Date = appointment.Schedule.Start.Date.ToString("yyyy-MM-dd"),
				};
				ev.End = new EventDateTime
				{
					Date = appointment.Schedule.End.Date.ToString("yyyy-MM-dd"),
				};
			}
			else
			{
				ev.Start = new EventDateTime
				{
					DateTime = appointment.Schedule.Start,
					TimeZone = TZConvert.WindowsToIana(appointment.Schedule.StartTimeZone.Id),
				};
				ev.End = new EventDateTime
				{
					DateTime = appointment.Schedule.End,
					TimeZone = TZConvert.WindowsToIana(appointment.Schedule.EndTimeZone.Id),
				};
			}

			return ev;
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
			if (recurrence.Exceptions != null)
			{
				string startTime = schedule.Start.ToString("HHmmss");
				string startTimezone = TZConvert.WindowsToIana(schedule.StartTimeZone.Id);
				foreach (DateTime exceptionDate in recurrence.Exceptions.Where(kv => kv.Value == null).Select(kv => kv.Key))
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
	}
}
