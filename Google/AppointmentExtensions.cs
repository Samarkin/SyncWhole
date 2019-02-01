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
					TimeZone = appointment.Schedule.StartTimeZone,
				},
				End = new EventDateTime
				{
					DateTime = appointment.Schedule.End,
					TimeZone = appointment.Schedule.EndTimeZone,
				},
				Recurrence =  appointment.Schedule.Recurrence.ToRfc5545RuleList(),
			};
		}

		public static IList<string> ToRfc5545RuleList(this IRecurrenceSchedule schedule)
		{
			if (schedule == null)
			{
				return null;
			}

			var sb = new StringBuilder();
			sb.Append("RRULE:FREQ=");
			switch (schedule.Frequency)
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
			if (schedule.Count.HasValue)
			{
				sb.Append($";COUNT={schedule.Count.Value}");
			}
			if (schedule.Interval.HasValue)
			{
				sb.Append($";INTERVAL={schedule.Interval.Value}");
			}
			if (schedule.Until.HasValue)
			{
				sb.Append($";UNTIL={schedule.Until.Value.Date:yyyyMMddT000000Z}"); // TODO: is UTC okay?
			}
			if (schedule.WeekDay.HasValue)
			{
				sb.Append($";BYDAY={ToRfc5545String(schedule.WeekDay.Value)}");
			}
			if (schedule.YearMonth.HasValue)
			{
				sb.Append($";BYMONTH={schedule.YearMonth.Value}");
			}
			if (schedule.MonthDay.HasValue)
			{
				sb.Append($";BYMONTHDAY={schedule.MonthDay.Value}");
			}
			return new[] {sb.ToString()};
		}

		private static string ToRfc5545String(WeekDay weekDay)
		{
			return string.Join(",", WeekDayMap.Where(kv => weekDay.HasFlag(kv.Key)).Select(kv => kv.Value));
		}
	}
}
