using System;

namespace SyncWhole.Common
{
	public interface IAppointmentSchedule
	{
		bool AllDay { get; }
		DateTime Start { get; }
		DateTime End { get; }
	}
}