using UnityEngine;
using System.Collections;

namespace OmniLibrary
{
	[RequireComponent(typeof(Rigidbody))]
	/// <summary>
	/// This MonoBehaviour uses drag as well as hard clamping to limit the velocity of a rigidbody.
	/// 
	/// Original by "Ehren" @ http://vonlehecreative.wordpress.com/2010/02/02/unity-resource-velocitylimiter/
	/// C# port by Dan Saul.
	/// </summary>
	public class VelocityLimiter : MonoBehaviour
	{
		/// <summary>
		/// The velocity at which drag should begin being applied.
		/// </summary>
		public float dragStartVelocity;
		
		/// <summary>
		/// The velocity at which drag should equal maxDrag.
		/// </summary>
		public float dragMaxVelocity;
		
		/// <summary>
		/// The maximum allowed velocity. The velocity will be clamped to keep 
		/// it from exceeding this value. (Note: this value should be greater than
		/// or equal to dragMaxVelocity.)
		/// </summary>
		public float maxVelocity;
		
		/// <summary>
		/// The maximum drag to apply. This is the value that will 
		/// be applied if the velocity is equal or greater
		/// than dragMaxVelocity. Between the start and max velocities,
		/// the drag applied will go from 0 to maxDrag, increasing
		/// the closer the velocity gets to dragMaxVelocity.
		/// </summary>
		public float maxDrag = 1f;
		
		/// <summary>
		/// The original drag of the object, which we use if the velocity
		/// is below dragStartVelocity.
		/// </summary>
		private float originalDrag;
		
		/// <summary>
		/// Cache the rigidbody to avoid GetComponent calls behind the scenes.
		/// </summary>
		private Rigidbody rb;
		
		// Cached values used in FixedUpdate
		private float sqrDragStartVelocity;
		private float sqrDragVelocityRange;
		private float sqrMaxVelocity;
		
		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// For more info, see:
		/// http://unity3d.com/support/documentation/ScriptReference/MonoBehaviour.Awake.html
		/// </summary>
		void Awake()
		{
			originalDrag = GetComponent<Rigidbody>().drag;
			rb = GetComponent<Rigidbody>();
			
			RefreshInternalVariables();
		}
		
		public void RefreshInternalVariables()
		{
			// Calculate cached values
			// Sets the threshold values and calculates cached variables used in FixedUpdate.
			// Outside callers who wish to modify the thresholds should use this function. Otherwise,
			// the cached values will not be recalculated.
			sqrDragStartVelocity = dragStartVelocity * dragStartVelocity;
			sqrDragVelocityRange = (dragMaxVelocity * dragMaxVelocity) - sqrDragStartVelocity;
			sqrMaxVelocity = maxVelocity * maxVelocity;
		}
		
		/// <summary>
		/// FixedUpdate is a built-in unity function that is called every fixed framerate frame.
		/// We use FixedUpdate instead of Update here because the docs recommend doing so when
		/// dealing with rigidbodies.
		/// For more info, see:
		/// http://unity3d.com/support/documentation/ScriptReference/MonoBehaviour.FixedUpdate.html
		/// 
		/// We limit the velocity here to account for gravity and to allow the drag to be relaxed 
		/// over time, even if no collisions are occurring.
		/// </summary>
		void FixedUpdate()
		{
			Vector3 v = rb.velocity;
			
			// We use sqrMagnitude instead of magnitude for performance reasons.
			float vSqr = v.sqrMagnitude;
			
			if(vSqr > sqrDragStartVelocity) {
				GetComponent<Rigidbody>().drag = Mathf.Lerp(originalDrag, maxDrag, Mathf.Clamp01((vSqr - sqrDragStartVelocity) / sqrDragVelocityRange));
				
				// Clamp the velocity, if necessary
				if(vSqr > sqrMaxVelocity){
					// Vector3.normalized returns this vector with a magnitude 
					// of 1. This ensures that we're not messing with the 
					// direction of the vector, only its magnitude.
					rb.velocity = v.normalized * maxVelocity;
				}
			} else {
				rb.drag = originalDrag;
			}
		}
	}
}

