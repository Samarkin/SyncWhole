namespace SyncWhole.Common
{
	public interface IAppointment
	{
		bool Busy { get; }
		bool Confirmed { get; }
		string Subject { get; }
		string Location { get; }
		IAppointmentSchedule Schedule { get; }
	}
}