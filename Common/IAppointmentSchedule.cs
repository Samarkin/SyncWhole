using System;

namespace SyncWhole.Common
{
	public interface IAppointmentSchedule
	{
		bool AllDay { get; }
		DateTime Start { get; }
		string StartTimeZone { get; }
		DateTime End { get; }
		string EndTimeZone { get; }
		IRecurrenceSchedule Recurrence { get; }
	}
}