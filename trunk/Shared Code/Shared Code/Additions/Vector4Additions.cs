using UnityEngine;
using System.Collections;

namespace SharedCode
{
	public static class Vector4Additions
	{
		
		
		/**
			Returns the projection of this vector onto the given base.
		*/
		public static Vector4 Proj(this Vector4 vector, Vector4 baseVector)
		{
			var direction = baseVector.normalized;
			var magnitude = Vector2.Dot(vector, direction);
			
			return direction * magnitude;
		}
		
		/**
			Returns the rejection of this vector onto the given base.

			The sum of a vector's projection and rejection on a base is
			equal to the original vector.
		*/
		public static Vector4 Rej(this Vector4 vector, Vector4 baseVector)
		{
			return vector - vector.Proj(baseVector);
		}
	}
}