using System;
using System.Runtime.Remoting.Messaging;
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
		}

		public string Subject => _appointment.Subject;
		public string Location => _appointment.Location;
		public string UniqueId => _appointment.GlobalAppointmentID;
		public DateTime LastModifiedDateTime => _appointment.LastModificationTime;

		public override string ToString() => Subject;
	}
}