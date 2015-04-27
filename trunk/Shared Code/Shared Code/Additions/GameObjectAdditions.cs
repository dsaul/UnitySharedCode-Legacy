using UnityEngine;
using System;
using System.Collections;

namespace SharedCode
{
	public static class GameObjectAdditions
	{
		public static T EnsureComponent<T>(this GameObject go) where T : Component
		{
			T c = go.GetComponent<T>();
			if (null == c)
				return go.AddComponent<T>();
			return c;
		}
	}
}
