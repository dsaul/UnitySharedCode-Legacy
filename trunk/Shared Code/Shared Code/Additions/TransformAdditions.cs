using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

namespace OmniLibrary
{
	public static class TransformAdditions
	{
		

		/// <summary>
		/// Calculates the torque applied to a transform. (Extension Method)
		/// </summary>
		public static Vector3 CalculateTorque(this Transform transform, Vector3 force, Vector3 position)
		{
			Vector3 r = transform.position - position;
			return Vector3.Cross(force, r);
		}

		/// <summary>
		/// Calculates the force and torque applied to a transform. (Extension Method)
		/// </summary>
		public static Vector3 CalculateForceAndTorque(this Transform transform, Vector3 force, Vector3 position)
		{
			return transform.CalculateTorque(force, position) + force;
		}

		public static Vector3 GetRandomPointAroundTransform(this Transform t, bool local)
		{
			Vector3 localPoint = Vector3.forward.RotateAroundPivot(Vector3.zero, Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 360), 0)));
			return true == local ? localPoint : t.TransformPoint(localPoint);
		}

		public static Vector3 GetRandomPointAroundTransformMultiplied(this Transform t, float min, float max, bool local)
		{
			Vector3 pointLocal = GetRandomPointAroundTransform(t, true);

			float x = pointLocal.x;
			float y = pointLocal.y;
			float z = pointLocal.z;
			float random = UnityEngine.Random.Range(min, max);
			x *= random;
			y *= random;

			return true == local ? new Vector3(x, y, z) : t.TransformPoint(x, y, z);
		}

	}






















































}