using System;
using Google.Apis.Calendar.v3.Data;
using SyncWhole.Common;

namespace SyncWhole.Google
{
	public class GoogleCalendarAppointment : ILoadedAppointment
	{
		private readonly Event _event;

		public GoogleCalendarAppointment(Event @event)
		{
			_event = @event;
		}

		public string Subject => _event.Summary;
		public string Location => _event.Location;
		public string UniqueId => _event.ICalUID;
		public DateTime LastModifiedDateTime => _event.Updated.GetValueOrDefault(DateTime.MinValue);

		public override string ToString() => Subject;
	}
}