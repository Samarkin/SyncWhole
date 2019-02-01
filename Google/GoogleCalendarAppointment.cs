using System;
using System.Collections.Generic;
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
			Schedule = @event.Start.DateTime.HasValue && @event.End.DateTime.HasValue
				? new AppointmentSchedule(@event)
				: null;
		}

		public bool Busy => string.Equals(_event.Transparency, "opaque", StringComparison.OrdinalIgnoreCase);
		public bool Confirmed => string.Equals(_event.Status, "confirmed", StringComparison.OrdinalIgnoreCase);
		public string Subject => _event.Summary;
		public string Location => _event.Location;
		public IAppointmentSchedule Schedule { get; }
		public string UniqueId => _event.ICalUID;
		public DateTime LastModifiedDateTime => _event.Updated.GetValueOrDefault(DateTime.MinValue);

		public string GoogleCalendarEventId => _event.Id;
		public bool Deleted => string.Equals(_event.Status, "cancelled", StringComparison.OrdinalIgnoreCase);

		public override string ToString() => Subject ?? "<null>";

		private class AppointmentSchedule : IAppointmentSchedule
		{
			private readonly Event _event;

			public AppointmentSchedule(Event @event)
			{
				_event = @event;
				// ReSharper disable PossibleInvalidOperationException - we checked before calling the constructor
				Start = _event.Start.DateTime.Value;
				End = _event.End.DateTime.Value;
				// ReSharper restore PossibleInvalidOperationException
				AllDay = Start == Start.Date && End == End.Date; // starts and ends at midnight
			}

			public bool AllDay { get; }
			public DateTime Start { get; }
			public string StartTimeZone => _event.Start.TimeZone;
			public DateTime End { get; }
			public string EndTimeZone => _event.End.TimeZone;
			public IRecurrenceSchedule Recurrence => null; // TODO
		}
	}
}