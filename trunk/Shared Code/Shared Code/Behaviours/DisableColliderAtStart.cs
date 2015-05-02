using UnityEngine;
using System.Collections;

namespace SharedCode.Behaviours
{
	namespace Behaviours
	{
		public class DisableColliderAtStart : MonoBehaviour {
			void Start () {
				GetComponent<Collider>().enabled = false;
			}
		}
		
	}
}































