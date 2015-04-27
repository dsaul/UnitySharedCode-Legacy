using UnityEngine;
using System.Collections;

namespace OmniLibrary
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































