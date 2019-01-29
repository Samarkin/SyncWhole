using System;

namespace SyncWhole.Common
{
	public interface ILoadedAppointment : IAppointment
	{
		string UniqueId { get; }
		DateTime LastModifiedDateTime { get; }
	}
}