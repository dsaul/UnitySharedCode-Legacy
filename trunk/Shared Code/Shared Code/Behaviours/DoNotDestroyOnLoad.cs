using UnityEngine;
using System.Collections;

namespace OmniLibrary
{
	namespace Behaviours
	{
		public class DoNotDestroyOnLoad : MonoBehaviour
		{
			void Start () {
				DontDestroyOnLoad(gameObject);
			}
		}
		
	}
}

































