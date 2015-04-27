using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OmniLibrary
{
	public static class IEnumerableAdditions
	{
		/**
			Returns a random element from the list.
		*/
		public static T RandomItem<T>(this IEnumerable<T> source)
		{
			return source.SampleRandom(1).First();
		}
		
		/**
			Returns a random sample from the list.
		*/
		public static IEnumerable<T> SampleRandom<T>(this IEnumerable<T> source, int sampleCount)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			if (sampleCount < 0)
				throw new ArgumentOutOfRangeException("sampleCount");

			if (sampleCount == 0)
				return new T[0];
			
			/* Reservoir sampling. */
			var samples = new List<T>();
			
			//Must be 1, otherwise we have to use Range(0, i + 1)
			var i = 1;

			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (i <= sampleCount)
					{
						samples.Add(enumerator.Current);
					}
					else
					{
						// Randomly replace elements in the reservoir with a decreasing probability.
						var r = UnityEngine.Random.Range(0, i);

						if (r < sampleCount)
						{
							samples[r] = enumerator.Current;
						}
					}

					i++;
				}
			}
			
			return samples;
		}
		
	}
}