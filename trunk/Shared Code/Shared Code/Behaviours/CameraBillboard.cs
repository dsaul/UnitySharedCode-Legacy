using UnityEngine;
using System.Collections;

namespace SharedCode
{
	public class CameraBillboard : MonoBehaviour
	{
		// the camera the the game object will be bill boarded to.
		public Camera m_Camera;
		// if true the game object will be positioned just in front of the cameras near clip plane
		public bool PositionInFrontOfCamera;
		// the offset to position the object when PositionInFrontOfCamera is true
		public float Offset = 0.001f;
		
		void Awake()
		{
			// if no camera has been specified just use main camera
			if (m_Camera == null) m_Camera = Camera.main;
		}
		
		void Update()
		{
			// get forward vector of the camera and normalize it
			var vec = m_Camera.transform.forward;
			vec.Normalize();
			
			// set the position of the game object just inside the cameras near clipping plane so it blocks the camera view
			if (this.PositionInFrontOfCamera) this.transform.position = m_Camera.transform.position + (vec * (m_Camera.nearClipPlane + this.Offset));
			
			// orient the game object to look at the camera
			this.transform.LookAt(this.transform.position + m_Camera.transform.rotation * Vector3.back, m_Camera.transform.rotation * Vector3.up);
		}
	}
}
