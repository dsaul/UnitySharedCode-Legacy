using UnityEngine;
using System.Collections;

namespace SharedCode
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

































