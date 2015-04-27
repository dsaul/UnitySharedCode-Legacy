using UnityEngine;
using System.Collections;
using System;

public static class FloatAdditions
{
	public static float MoveTowards(this float current, float target, float maxDelta)
	{
		if (Mathf.Abs(target - current) <= maxDelta)
		{
			return target;
		}
		return current + Mathf.Sign(target - current) * maxDelta;
	}

	public static bool NearlyEqual(this float a, float b, float epsilon)
	{
		float absA = Math.Abs(a);
		float absB = Math.Abs(b);
		float diff = Math.Abs(a - b);

		if (a == b)
		{ // shortcut, handles infinities
			return true;
		}
		else if (a == 0 || b == 0 || diff < float.MinValue)
		{
			// a or b is zero or both are extremely close to it
			// relative error is less meaningful here
			return diff < (epsilon * float.MinValue);
		}
		else
		{ // use relative error
			return diff / (absA + absB) < epsilon;
		}
	}
}