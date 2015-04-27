using UnityEngine;
using System.Collections;


namespace SharedCode
{
	public static class Vector3Additions
	{
		//http://answers.unity3d.com/questions/192261/finding-shortest-line-segment-between-two-rays-clo.html
		public static float ClosestTimeOfApproach(Vector3 pos1, Vector3 vel1, Vector3 pos2, Vector3 vel2)
		{
			//float t = 0;
			var dv = vel1 - vel2;
			var dv2 = Vector3.Dot(dv, dv);
			if (dv2 < 0.0000001f)      // the tracks are almost parallel
			{
				return 0.0f; // any time is ok.  Use time 0.
			}
			
			var w0 = pos1 - pos2;
			return (-Vector3.Dot(w0, dv)/dv2);
		}
		
		public static float ClosestDistOfApproach(Vector3 pos1, Vector3 vel1, Vector3 pos2, Vector3 vel2, out Vector3 p1, out Vector3 p2)
		{
			var t = ClosestTimeOfApproach(pos1,vel1,pos2,vel2);
			p1 = pos1 + (t * vel1);
			p2 = pos2 + (t*vel2);
			
			return Vector3.Distance(p1, p2);           // distance at CPA
		}
		
		public static Vector3 ClosestPointOfApproach(Vector3 pos1, Vector3 vel1, Vector3 pos2, Vector3 vel2)
		{
			var t = ClosestTimeOfApproach(pos1, vel1, pos2, vel2);
			if (t<0) // don't detect approach points in the past, only in the future;
			{
				return (pos1); 
			}
		
			return (pos1 + (t * vel1));
		
		}
		
		// Something like this: attackWaypoint.RotateAroundPivot(target.transform.position,Quaternion.Euler(new Vector3(0,33,0)))
		public static Vector3 RotateAroundPivot(this Vector3 point, Vector3 pivot, Quaternion angle)
		{
			return angle * (point - pivot) + pivot;
		}
		
		/// <summary>
		/// gets the square distance between two vector3 positions. this is much faster that Vector3.distance.
		/// </summary>
		/// <param name="first">first point</param>
		/// <param name="second">second point</param>
		/// <returns>squared distance</returns>
		public static float SqrDistance(this Vector3 first, Vector3 second)
		{
			return Vector3.SqrMagnitude(first - second);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static Vector3 MidPoint(this Vector3 first, Vector3 second)
		{
			return new Vector3((first.x + second.x)*0.5f, (first.y + second.y)*0.5f, (first.z + second.z)*0.5f);
		}
		
		/// <summary>
		/// get the square distance from a point to a line segment.
		/// </summary>
		/// <param name="point">point to get distance to</param>
		/// <param name="lineP1">line segment start point</param>
		/// <param name="lineP2">line segment end point</param>
		/// <param name="closestPoint">set to either 1, 2, or 4, determining which end the point is closest to (p1, p2, or the middle)</param>
		/// <returns></returns>
		public static float SqrLineDistance(this Vector3 point, Vector3 lineP1, Vector3 lineP2, out int closestPoint)
		{
			
			Vector3 v = lineP2 - lineP1;
			Vector3 w = point - lineP1;
			
			float c1 = Vector3.Dot(w, v);
			
			if (c1 <= 0) //closest point is p1
			{
				closestPoint = 1;
				return SqrDistance(point, lineP1);
			}
			
			float c2 = Vector3.Dot(v, v);
			if (c2 <= c1) //closest point is p2
			{
				closestPoint = 2;
				return SqrDistance(point, lineP2);
			}
			
			
			float b = c1/c2;
			
			Vector3 pb = lineP1 + b*v;
			{
				closestPoint = 4;
				return SqrDistance(point, pb);
			}
		}
		
		/// <summary>
		/// Absolute value of components
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static Vector3 Abs(this Vector3 v)
		{
			return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
		}
		
		/// <summary>
		/// Vector3.Project, onto a plane
		/// </summary>
		/// <param name="v"></param>
		/// <param name="planeNormal"></param>
		/// <returns></returns>
		public static Vector3 ProjectOntoPlane(this Vector3 v, Vector3 planeNormal)
		{
			return v - Vector3.Project(v, planeNormal);
		}
		
		/// <summary>
		/// Gets the normal of the triangle formed by the 3 vectors
		/// </summary>
		/// <param name="vec1"></param>
		/// <param name="vec2"></param>
		/// <param name="vec3"></param>
		/// <returns></returns>
		public static Vector3 Vector3Normal(Vector3 vec1, Vector3 vec2, Vector3 vec3)
		{
			return Vector3.Cross((vec3 - vec1), (vec2 - vec1));
		}
		
		/// <summary>
		/// Using the magic of 0x5f3759df
		/// </summary>
		/// <param name="vec1"></param>
		/// <returns></returns>
		public static Vector3 FastNormalized(this Vector3 vec1)
		{
			var componentMult = MathAdditions.FastInvSqrt(vec1.sqrMagnitude);
			return new Vector3(vec1.x*componentMult, vec1.y*componentMult, vec1.z*componentMult);
		}
		
		/// <summary>
		/// Gets the center of two points
		/// </summary>
		/// <param name="vec1"></param>
		/// <param name="vec2"></param>
		/// <returns></returns>
		public static Vector3 Center(Vector3 vec1, Vector3 vec2)
		{
			return new Vector3((vec1.x + vec2.x)/2, (vec1.y + vec2.y)/2, (vec1.z + vec2.z)/2);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="vec"></param>
		/// <returns></returns>
		public static bool IsNaN(this Vector3 vec)
		{
			return float.IsNaN(vec.x*vec.y*vec.z);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="points"></param>
		/// <returns></returns>
		public static Vector3 Center(this Vector3[] points)
		{
			Vector3 ret = Vector3.zero;
			for (int i = 0; i < points.Length; i++)
			{
				ret += points[i];
			}
			ret /= points.Length;
			return ret;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dir1"></param>
		/// <param name="dir2"></param>
		/// <param name="axis"></param>
		/// <returns></returns>
		public static float AngleAroundAxis(Vector3 dir1, Vector3 dir2, Vector3 axis)
		{
			dir1 = dir1 - Vector3.Project(dir1, axis);
			dir2 = dir2 - Vector3.Project(dir2, axis);
			
			float angle = Vector3.Angle(dir1, dir2);
			return angle*(Vector3.Dot(axis, Vector3.Cross(dir1, dir2)) < 0 ? -1 : 1);
		}
		
		/// <summary>
		/// Returns a random direction in a cone. a spread of 0 is straight, 0.5 is 180*
		/// </summary>
		/// <param name="spread"></param>
		/// <param name="forward">must be unit</param>
		/// <returns></returns>
		public static Vector3 RandomDirection(float spread, Vector3 forward)
		{
			return Vector3.Slerp(forward, Random.onUnitSphere, spread);
		}
		
		/// <summary>
		/// test if a Vector3 is close to another Vector3 (due to floating point inprecision)
		/// compares the square of the distance to the square of the range as this
		/// avoids calculating a square root which is much slower than squaring the range
		/// </summary>
		/// <param name="val"></param>
		/// <param name="about"></param>
		/// <param name="range"></param>
		/// <returns></returns>
		public static bool Approx(Vector3 val, Vector3 about, float range)
		{
			return ((val - about).sqrMagnitude < range * range);
		}
		
		/// <summary>
		/// Find a point on the infinite line nearest to point
		/// </summary>
		/// <param name="lineStart"></param>
		/// <param name="lineEnd"></param>
		/// <param name="point"></param>
		/// <returns></returns>
		public static Vector3 NearestPoint(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			Vector3 lineDirection = Vector3.Normalize(lineEnd - lineStart);
			float closestPoint = Vector3.Dot((point - lineStart), lineDirection) / Vector3.Dot(lineDirection, lineDirection);
			return lineStart + (closestPoint * lineDirection);
		}
		
		/// <summary>
		/// find a point on the line segment nearest to point
		/// </summary>
		/// <param name="lineStart"></param>
		/// <param name="lineEnd"></param>
		/// <param name="point"></param>
		/// <returns></returns>
		public static Vector3 NearestPointStrict(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			Vector3 fullDirection = lineEnd - lineStart;
			Vector3 lineDirection = Vector3.Normalize(fullDirection);
			float closestPoint = Vector3.Dot((point - lineStart), lineDirection) / Vector3.Dot(lineDirection, lineDirection);
			return lineStart + (Mathf.Clamp(closestPoint, 0.0f, Vector3.Magnitude(fullDirection)) * lineDirection);
		}
		
		/// <summary>
		/// Calculates the intersection line segment between 2 lines (not segments).
		/// Returns false if no solution can be found.
		/// </summary>
		/// <returns></returns>
		public static bool CalculateLineLineIntersection(Vector3 line1Point1, Vector3 line1Point2,
		                                                 Vector3 line2Point1, Vector3 line2Point2, out Vector3 resultSegmentPoint1, out Vector3 resultSegmentPoint2)
		{
			// Algorithm is ported from the C algorithm of 
			// Paul Bourke at http://local.wasp.uwa.edu.au/~pbourke/geometry/lineline3d/
			resultSegmentPoint1 = new Vector3(0, 0, 0);
			resultSegmentPoint2 = new Vector3(0, 0, 0);
			
			var p1 = line1Point1;
			var p2 = line1Point2;
			var p3 = line2Point1;
			var p4 = line2Point2;
			var p13 = p1 - p3;
			var p43 = p4 - p3;
			
			if (p4.sqrMagnitude < float.Epsilon)
			{
				return false;
			}
			var p21 = p2 - p1;
			if (p21.sqrMagnitude < float.Epsilon)
			{
				return false;
			}
			
			var d1343 = p13.x * p43.x + p13.y * p43.y + p13.z * p43.z;
			var d4321 = p43.x * p21.x + p43.y * p21.y + p43.z * p21.z;
			var d1321 = p13.x * p21.x + p13.y * p21.y + p13.z * p21.z;
			var d4343 = p43.x * p43.x + p43.y * p43.y + p43.z * p43.z;
			var d2121 = p21.x * p21.x + p21.y * p21.y + p21.z * p21.z;
			
			var denom = d2121 * d4343 - d4321 * d4321;
			if (Mathf.Abs(denom) < float.Epsilon)
			{
				return false;
			}
			var numer = d1343 * d4321 - d1321 * d4343;
			
			var mua = numer / denom;
			var mub = (d1343 + d4321 * (mua)) / d4343;
			
			resultSegmentPoint1.x = p1.x + mua * p21.x;
			resultSegmentPoint1.y = p1.y + mua * p21.y;
			resultSegmentPoint1.z = p1.z + mua * p21.z;
			resultSegmentPoint2.x = p3.x + mub * p43.x;
			resultSegmentPoint2.y = p3.y + mub * p43.y;
			resultSegmentPoint2.z = p3.z + mub * p43.z;
			
			return true;
		}

		/// <summary>
		/// Determines whether a Vector3 is valid (all values are not equal to Infinity or NaN). (Extension Method)
		/// </summary>
		public static bool IsValid(this Vector3 input)
		{
			return (!float.IsNaN(input.x)
			        && !float.IsNaN(input.y)
			        && !float.IsNaN(input.z)
			        && input.x != Mathf.Infinity
			        && input.y != Mathf.Infinity
			        && input.z != Mathf.Infinity);
		}

		/**
			Returns the projection of this vector onto the given base.
		*/
		public static Vector3 Proj(this Vector3 vector, Vector3 baseVector)
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
		public static Vector3 Rej(this Vector3 vector, Vector3 baseVector)
		{
			return vector - vector.Proj(baseVector);
		}

	}
	
	
}