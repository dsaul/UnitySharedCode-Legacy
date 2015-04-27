using UnityEngine;
using System.Collections;

namespace SharedCode
{
	public class MatchObjectPosition : MonoBehaviour
	{
		public Transform targetTransform;
		
		Vector3 offset = Vector3.zero;
		void Start()
		{
			//offset = target.transform.position - transform.position;
		}
		
		void LateUpdate()
		{
			if (targetTransform)
			{
				transform.rotation = targetTransform.rotation;
				transform.position = targetTransform.position - (transform.rotation * offset);
			}
		}
	}
	
}
