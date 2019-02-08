using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Outlook;
using SyncWhole.Common;
using TimeZoneConverter;

namespace SyncWhole.Outlook
{
	public sealed class OutlookAppointment : ILoadedAppointment
	{
		private readonly AppointmentItem _appointment;

		public OutlookAppointment(AppointmentItem appointment)
		{
			_appointment = appointment;
			Schedule = new AppointmentSchedule(appointment);
		}

		public bool Busy => _appointment.BusyStatus == OlBusyStatus.olBusy;
		public bool Confirmed => _appointment.BusyStatus != OlBusyStatus.olTentative;
		public string Subject => _appointment.Subject;
		public string Location => _appointment.Location;
		public IAppointmentSchedule Schedule { get; }
		public string UniqueId => "OU" + _appointment.GlobalAppointmentID;
		public DateTime LastModifiedDateTime => _appointment.LastModificationTime;

		public override string ToString() => Subject ?? "<null>";

		private class AppointmentSchedule : IAppointmentSchedule
		{
			private readonly AppointmentItem _appointment;

			public AppointmentSchedule(AppointmentItem appointment)
			{
				_appointment = appointment;
				if (appointment.IsRecurring && appointment.RecurrenceState == OlRecurrenceState.olApptMaster)
				{
					Recurrence = new AppointmentRecurrence(_appointment.GetRecurrencePattern());
				}
			}

			public bool AllDay => _appointment.AllDayEvent;
			public DateTime Start => _appointment.StartInStartTimeZone;
			public TimeZoneInfo StartTimeZone => TimeZoneInfo.FindSystemTimeZoneById(_appointment.StartTimeZone.ID);
			public DateTime End => _appointment.EndInEndTimeZone;
			public TimeZoneInfo EndTimeZone => TimeZoneInfo.FindSystemTimeZoneById(_appointment.EndTimeZone.ID);
			public IRecurrenceSchedule Recurrence { get; }
		}

		private class AppointmentRecurrence : IRecurrenceSchedule
		{
			private static readonly Dictionary<OlDaysOfWeek, WeekDay> WeekDayMap = new Dictionary<OlDaysOfWeek, WeekDay>
			{
				{OlDaysOfWeek.olSunday, Common.WeekDay.Sunday},
				{OlDaysOfWeek.olMonday, Common.WeekDay.Monday},
				{OlDaysOfWeek.olTuesday, Common.WeekDay.Tuesday},
				{OlDaysOfWeek.olWednesday, Common.WeekDay.Wednesday},
				{OlDaysOfWeek.olThursday, Common.WeekDay.Thursday},
				{OlDaysOfWeek.olFriday, Common.WeekDay.Friday},
				{OlDaysOfWeek.olSaturday, Common.WeekDay.Saturday},
			};

			public AppointmentRecurrence(RecurrencePattern pattern)
			{
				switch (pattern.RecurrenceType)
				{
					case OlRecurrenceType.olRecursDaily:
						Frequency = RecurrenceFrequency.Daily;
						break;
					case OlRecurrenceType.olRecursWeekly:
						Frequency = RecurrenceFrequency.Weekly;
						WeekDay = ConvertWeekDays(pattern.DayOfWeekMask);
						break;
					case OlRecurrenceType.olRecursMonthly:
						Frequency = RecurrenceFrequency.Monthly;
						MonthDay = pattern.DayOfMonth;
						break;
					case OlRecurrenceType.olRecursYearly:
						Frequency = RecurrenceFrequency.Yearly;
						MonthDay = pattern.DayOfMonth;
						YearMonth = pattern.MonthOfYear;
						break;
					default:
						throw new ArgumentOutOfRangeException($"Recurrence type {pattern.RecurrenceType} is not supported");
				}
				Interval = pattern.Interval;
				if (!pattern.NoEndDate)
				{
					if (pattern.Occurrences != 0)
					{
						Count = pattern.Occurrences;
					}
					else
					{
						Until = pattern.PatternEndDate;
					}
				}
				if (pattern.Exceptions.Count > 0)
				{
					var exs = new Dictionary<DateTime, ILoadedAppointment>();
					foreach (Microsoft.Office.Interop.Outlook.Exception ex in pattern.Exceptions)
					{
						exs[ex.OriginalDate] = ex.Deleted ? null : new OutlookAppointment(ex.AppointmentItem);
					}
					Exceptions = exs;
				}
			}

			public RecurrenceFrequency Frequency { get; }
			public DateTime? Until { get; }
			public int? Count { get; }
			public int? Interval { get; }
			public int? MonthDay { get; }
			public int? YearMonth { get; }
			public WeekDay? WeekDay { get; }
			public IReadOnlyDictionary<DateTime, ILoadedAppointment> Exceptions { get; }

			private static WeekDay ConvertWeekDays(OlDaysOfWeek olDaysOfWeek)
			{
				WeekDay result = 0;
				foreach (var kv in WeekDayMap)
				{
					if (olDaysOfWeek.HasFlag(kv.Key))
					{
						result |= kv.Value;
					}
				}
				return result;
			}
		}
	}
}