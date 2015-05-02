using UnityEngine;
using System.Collections;

namespace SharedCode.Behaviours
{
	public class DoNotDestroyOnLoad : MonoBehaviour
	{
		void Start()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}

































