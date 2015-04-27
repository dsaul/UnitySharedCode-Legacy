using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SharedCode
{
	public static class IListAdditions
	{
		/**	
			Shuffles a list.
		*/
		public static void Shuffle<T>(this IList<T> source)  
		{  
			var n = source.Count;  
			
			while (n > 1) 
			{  
				n--;  
				var k = Random.Range(0, n + 1);  
				var value = source[k];  
				source[k] = source[n];  
				source[n] = value;  
			}  
		}

		public static void Reverse_NoHeapAlloc<T>(this List<T> list)
		{
			int count = list.Count;

			for (int i = 0; i < count / 2; i++)
			{
				T tmp = list[i];
				list[i] = list[count - i - 1];
				list[count - i - 1] = tmp;
			}
		}
	}
}