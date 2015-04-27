using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SharedCode
{
	public static class ArrayAdditions
	{
		public static T TryGet<T>(this T[] arr, int index)
		{
			if (index < arr.Length)
				return arr[index];
			return default(T);
		}
	}
}