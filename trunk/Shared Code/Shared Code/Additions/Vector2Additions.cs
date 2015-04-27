using UnityEngine;
using System.Collections;

namespace SharedCode
{
	public static class Vector2Additions
	{
		public static Vector2 NearestPointStrict(Vector2 lineStart, Vector2 lineEnd, Vector2 point)
		{
			var fullDirection = lineEnd - lineStart;
			var lineDirection = fullDirection.normalized;
			var closestPoint = Vector2.Dot((point - lineStart), lineDirection) / Vector2.Dot(lineDirection, lineDirection);
			return lineStart + (Mathf.Clamp(closestPoint, 0, fullDirection.magnitude) * lineDirection);
		}

		static readonly float COS45 = (float)Mathf.Cos (Mathf.PI / 4);
		static readonly float SIN45 = (float)Mathf.Sin (Mathf.PI / 4);

		static public Vector2 Rotate90 (this Vector2 vec) {
			return new Vector2 (-vec.y, vec.x);
		}
		
		static public Vector2 Rotate45 (this Vector2 vec) {
			return new Vector2 (vec.x * COS45 - vec.y * SIN45 , vec.x * SIN45 + vec.y * COS45);
		}
		
		static public float Dot (this Vector2 a, Vector2 b) {
			return a.x*b.x + a.y*b.y;
		}
		
		static public Vector2 Reflect (this Vector2 vec, Vector2 unitNormal) { return vec.Reflect (unitNormal, 1, 1); }
		static public Vector2 Reflect (this Vector2 vec, Vector2 unitNormal, float normalScale, float tangentScale) 
		{
			Vector2 unitTangent = unitNormal.Rotate90 ();
			
			float normComponent = vec.Dot (unitNormal)  * -normalScale;
			float tangComponent = vec.Dot (unitTangent) *  tangentScale;
			
			return unitNormal * normComponent + unitTangent * tangComponent;
		}

		/// <summary>
		/// Determines whether a Vector2 is valid (all values are not equal to Infinity or NaN). (Extension Method)
		/// </summary>
		public static bool IsValid(this Vector2 input)
		{
			return (!float.IsNaN(input.x)
			        && !float.IsNaN(input.y)
			        && input.x != Mathf.Infinity
			        && input.y != Mathf.Infinity);
		}

		/**
			Returns the vector rotated 90 degrees counter-clockwise. This vector is
			always perpendicular to the given vector.

			The perp dot product can be caluclted using this:
				var perpDotPorpduct = Vector2.Dot(v1.Perp(), v2);
		*/
		public static Vector2 Perp(this Vector2 vector)
		{
			return new Vector2(-vector.y, vector.x);
		}
		
		/**
			Returns the projection of this vector onto the given base.
		*/
		public static Vector2 Proj(this Vector2 vector, Vector2 baseVector)
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
		public static Vector2 Rej(this Vector2 vector, Vector2 baseVector)
		{
			return vector - vector.Proj(baseVector);
		}

		public static float SqrDistance(this Vector2 first, Vector2 second)
		{
			return Vector2.SqrMagnitude(first - second);
		}

	}
}