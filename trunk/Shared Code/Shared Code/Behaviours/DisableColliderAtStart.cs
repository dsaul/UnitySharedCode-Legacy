using UnityEngine;
using System.Collections;

namespace SharedCode
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































