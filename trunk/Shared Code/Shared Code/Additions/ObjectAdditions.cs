using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace OmniLibrary
{
	public static class ObjectAdditions
	{
		public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
		{
			if (val.CompareTo(min) < 0) return min;
			else if(val.CompareTo(max) > 0) return max;
			else return val;
		}
		
		// http://stackoverflow.com/a/5023279
		public static bool IsBetween<T>(this T item, T start, T end)
		{
			return Comparer<T>.Default.Compare(item, start) >= 0
				&& Comparer<T>.Default.Compare(item, end) <= 0;
		}
		
		public static void NoOp(this object o)
		{
			
		}
	}
}