using System;

namespace SyncWhole.Common
{
	public interface IAppointmentSchedule
	{
		bool AllDay { get; }
		DateTime Start { get; }
		TimeZoneInfo StartTimeZone { get; }
		DateTime End { get; }
		TimeZoneInfo EndTimeZone { get; }
		IRecurrenceSchedule Recurrence { get; }
	}
}