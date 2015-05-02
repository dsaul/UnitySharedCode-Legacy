using UnityEngine;
using System.Collections;

namespace SharedCode.Behaviours
{
	public class LookAtObject : MonoBehaviour
	{
		public GameObject target;
		
		void Update()
		{
			if (target != null)
				transform.LookAt(target.transform);
		}
	}
}

