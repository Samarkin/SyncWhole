using System;
using System.Collections.Generic;
using SyncWhole.Common;
using SyncWhole.Google;
using SyncWhole.Outlook;
using SyncWhole.Properties;

namespace SyncWhole
{
	public static class FactoryStore
	{
		private static readonly Dictionary<char, Func<string, IAppointmentSourceFactory>> SourceFactoryMap =
			new Dictionary<char, Func<string, IAppointmentSourceFactory>>
			{
				{'O', _ => new OutlookAdapterFactory()},
			};

		private static readonly Dictionary<char, Func<string, IAppointmentDestinationFactory>> DestinationFactoryMap =
			new Dictionary<char, Func<string, IAppointmentDestinationFactory>>
			{
				{'G', arg => new GoogleCalendarAdapterFactory(arg)},
			};

		private static T BuildFactoryFromString<T>(string serializedFactory, IReadOnlyDictionary<char, Func<string, T>> factoryMap)
		{
			if (string.IsNullOrEmpty(serializedFactory))
			{
				throw new ArgumentNullException(nameof(serializedFactory));
			}
			if (!factoryMap.TryGetValue(serializedFactory[0], out var func))
			{
				throw new ArgumentOutOfRangeException(nameof(serializedFactory),
					$"Unknown factory: \"{serializedFactory}\"");
			}
			return func(serializedFactory.Substring(1));
		}

		public static IAppointmentSourceFactory CurrentSource =>
			BuildFactoryFromString(Settings.Default.CalendarSource, SourceFactoryMap);

		public static IAppointmentDestinationFactory CurrentDestination =>
			BuildFactoryFromString(Settings.Default.CalendarDestination, DestinationFactoryMap);
	}
}
