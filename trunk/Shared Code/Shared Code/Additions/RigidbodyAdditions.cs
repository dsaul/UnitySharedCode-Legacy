using UnityEngine;
using System.Collections;

namespace OmniLibrary
{
	public static class RigidbodyAdditions
	{
		static public void CapSpeed (this Rigidbody rb, float speed)
		{
			if (rb.velocity.sqrMagnitude > speed*speed) {
				rb.AddForce (-rb.velocity.normalized * (rb.velocity.magnitude - speed), ForceMode.VelocityChange);
			}
		}

		/// <summary>
		/// Calculates the torque applied to a rigidbody. (Extension Method)
		/// </summary>
		public static Vector3 CalculateTorque(this Rigidbody rigidbody, Vector3 force, Vector3 position)
		{
			Vector3 r = rigidbody.position - position;
			return Vector3.Cross(force, r);
		}

		/// <summary>
		/// Calculates the force and torque applied to a rigidbody. (Extension Method)
		/// </summary>
		public static Vector3 CalculateForceAndTorque(this Rigidbody rigidbody, Vector3 force, Vector3 position)
		{
			return rigidbody.CalculateTorque(force, position) + force;
		}
	}
}
