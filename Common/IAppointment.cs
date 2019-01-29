namespace SyncWhole.Common
{
	public interface IAppointment
	{
		bool Busy { get; }
		string Subject { get; }
		string Location { get; }
		IAppointmentSchedule Schedule { get; }
	}
}