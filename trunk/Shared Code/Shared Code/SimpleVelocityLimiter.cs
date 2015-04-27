using UnityEngine;
using System.Collections;

namespace OmniLibrary
{
	[RequireComponent(typeof(Rigidbody))]
	/// <summary>
	/// http://vonlehecreative.wordpress.com/2010/02/02/unity-resource-velocitylimiter/
	/// This MonoBehaviour uses hard clamping to limit the velocity of a rigidbody.
	/// The maximum allowed velocity. The velocity will be clamped to keep 
	/// it from exceeding this value.
	/// </summary>
	public class SimpleVelocityLimiter : MonoBehaviour
	{
		float maxVelocity;
		
		/// <summary>
		/// The cached rigidbody reference.
		/// </summary>
		private Rigidbody rb;
		
		/// <summary>
		/// A cached copy of the squared max velocity. Used in FixedUpdate.
		/// </summary>
		private float sqrMaxVelocity;
		
		
		void Awake()
		{
			rb = GetComponent<Rigidbody>();
			SetMaxVelocity(maxVelocity);
		}
		
		/// <summary>
		/// Sets the max velocity and calculates the squared max velocity for use in FixedUpdate.
		/// Outside callers who wish to modify the max velocity should use this function. Otherwise,
		/// the cached squared velocity will not be recalculated.
		/// </summary>
		/// <param name="maxVelocity">Max velocity.</param>
		public void SetMaxVelocity(float maxVelocity)
		{
			this.maxVelocity = maxVelocity;
			sqrMaxVelocity = maxVelocity * maxVelocity;
		}
		
		void FixedUpdate()
		{
			var v = rb.velocity;
			// Clamp the velocity, if necessary
			// Use sqrMagnitude instead of magnitude for performance reasons.
			if(v.sqrMagnitude > sqrMaxVelocity){ // Equivalent to: GetComponent<Rigidbody>().velocity.magnitude > maxVelocity, but faster.
				// Vector3.normalized returns this vector with a magnitude 
				// of 1. This ensures that we're not messing with the 
				// direction of the vector, only its magnitude.
				rb.velocity = v.normalized * maxVelocity;
			}	
		}
	}
}
