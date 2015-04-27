using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace SharedCode
{
	public static class ListAdditions
	{
		/**
			Removes all the elements in the list that does not satisfy the predicate.
		*/
		public static void RemoveAllBut<T>(this List<T> source, Predicate<T> match)
		{
			Predicate<T> nomatch = item => !match(item);
			
			source.RemoveAll(nomatch);
		}

		public static T TryGet<T>(this List<T> arr, int index)
		{
			if (index < arr.Count)
				return arr[index];
			return default(T);
		}

		public static void AddAll<T>(this List<T> a, IEnumerable<T> other)
		{
			using (IEnumerator<T> enumerator = other.GetEnumerator())
				while (enumerator.MoveNext())
					a.Add(enumerator.Current);
		}

		
		public static void Resize<T>(this List<T> list, int sz, T c)
		{
			int cur = list.Count;
			if (sz < cur)
				list.RemoveRange(sz, cur - sz);
			else if (sz > cur)
			{
				if (sz > list.Capacity)//this bit is purely an optimisation, to avoid multiple automatic capacity changes.
					list.Capacity = sz;
				list.AddRange(Enumerable.Repeat(c, sz - cur));
			}
		}
		public static void Resize<T>(this List<T> list, int sz) where T : new()
		{
			Resize(list, sz, default(T));
		}
		
	}
}