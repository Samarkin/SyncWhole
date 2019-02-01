using System;
using System.Collections.Generic;

namespace SyncWhole
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<TResult> SmartZip<TSource, TResult>(this IEnumerable<TSource> first, IEnumerable<TSource> second,
			IEqualityComparer<TSource> comparer, Func<TSource, TSource, TResult> resultSelector)
		{
			var set = new Dictionary<TSource,TSource>(comparer);
			foreach (TSource x in second)
			{
				set[x] = x;
			}

			foreach (TSource x in first)
			{
				if (set.TryGetValue(x, out TSource y))
				{
					yield return resultSelector(x, y);
				}
			}
		}
	}
}