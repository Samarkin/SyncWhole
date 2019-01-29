using System.Collections.Generic;

namespace SyncWhole.Common
{
	public class LoadedAppointmentIdComparer : IEqualityComparer<ILoadedAppointment>
	{
		public static IEqualityComparer<ILoadedAppointment> Default = new LoadedAppointmentIdComparer();

		private LoadedAppointmentIdComparer()
		{
		}

		public bool Equals(ILoadedAppointment x, ILoadedAppointment y)
		{
			return x?.UniqueId == y?.UniqueId;
		}

		public int GetHashCode(ILoadedAppointment obj)
		{
			return obj?.UniqueId.GetHashCode() ?? 0;
		}
	}
}