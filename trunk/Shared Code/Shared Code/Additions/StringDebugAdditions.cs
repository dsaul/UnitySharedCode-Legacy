using UnityEngine;
using System.Collections;

namespace OmniLibrary
{
	public static class StringDebugAdditions
	{
		public static void Log(this string str, Object context, params Object[] args)
		{
			Debug.Log(string.Format(str,args),context);
		}
		
		public static void Log(this string str, params Object[] args)
		{
			Debug.Log(string.Format(str,args));
		}
		
		public static void LogWarning(this string str, Object context, params Object[] args)
		{
			Debug.LogWarning(string.Format(str,args),context);
		}
		
		public static void LogWarning(this string str, params Object[] args)
		{
			Debug.LogWarning(string.Format(str,args));
		}
		
		public static void LogError(this string str, Object context, params Object[] args)
		{
			Debug.LogError(string.Format(str,args),context);
		}
		
		public static void LogError(this string str, params Object[] args)
		{
			Debug.LogError(string.Format(str,args));
		}
	}
}