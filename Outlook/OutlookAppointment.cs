using System;
using Microsoft.Office.Interop.Outlook;
using SyncWhole.Common;

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
		public string Subject => _appointment.Subject;
		public string Location => _appointment.Location;
		public IAppointmentSchedule Schedule { get; }
		public string UniqueId => _appointment.GlobalAppointmentID;
		public DateTime LastModifiedDateTime => _appointment.LastModificationTime;

		public override string ToString() => Subject;

		private class AppointmentSchedule : IAppointmentSchedule
		{
			private readonly AppointmentItem _appointment;

			public AppointmentSchedule(AppointmentItem appointment)
			{
				_appointment = appointment;
			}

			public bool AllDay => _appointment.AllDayEvent;
			public DateTime Start => _appointment.Start;
			public DateTime End => _appointment.End;
		}
	}
}