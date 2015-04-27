using UnityEngine;
using System.Collections;
using System;

namespace OmniLibrary
{
	public static class EnumTryParse
	{
		public static bool TryParse<TEnum>(string value, out TEnum result)
			where TEnum : struct, IConvertible
		{
			var retValue = value == null ? 
				false : 
					Enum.IsDefined(typeof(TEnum), value);
			result = retValue ?
				(TEnum)Enum.Parse(typeof(TEnum), value) :
					default(TEnum);
			return retValue;
		}
	}
}