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
			Schedule = new AppointmentSchedule(@event);
		}

		public bool Busy => string.Equals(_event.Transparency, "opaque", StringComparison.OrdinalIgnoreCase);
		public string Subject => _event.Summary;
		public string Location => _event.Location;
		public IAppointmentSchedule Schedule { get; }
		public string UniqueId => _event.ICalUID;
		public DateTime LastModifiedDateTime => _event.Updated.GetValueOrDefault(DateTime.MinValue);

		public string GoogleCalendarEventId => _event.Id;

		public override string ToString() => Subject;

		private class AppointmentSchedule : IAppointmentSchedule
		{
			private readonly Event _event;

			public AppointmentSchedule(Event @event)
			{
				_event = @event;
				Start = _event.Start.DateTime.Value;
				End = _event.End.DateTime.Value;
				AllDay = Start == Start.Date && End == End.Date; // starts and ends at midnight
			}

			public bool AllDay { get; }
			public DateTime Start { get; }
			public DateTime End { get; }
		}
	}
}