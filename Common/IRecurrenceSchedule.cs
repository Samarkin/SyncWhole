using System;

namespace SyncWhole.Common
{
	public interface IRecurrenceSchedule
	{
		RecurrenceFrequency Frequency { get; }
		DateTime? Until { get; }
		int? Count { get; }
		int? Interval { get; }
		int? MonthDay { get; }
		int? YearMonth { get; }
		WeekDay? WeekDay { get; }
	}
}