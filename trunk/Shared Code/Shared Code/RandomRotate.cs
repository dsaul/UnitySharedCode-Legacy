using UnityEngine;
using System.Collections;

namespace OmniLibrary
{
	public class RandomRotate : MonoBehaviour
	{
		public float amtx =  0.0f;
		public float amty =  0.0f;
		public float amtz =  0.0f;
		
		private Transform this_transform;
		
		void Start()
		{
			amty  = Random.Range(Random.Range(4.0f,7.0f),-Random.Range(4.0f,7.0f));
			amtx  = Random.Range(Random.Range(4.0f,7.0f),-Random.Range(4.0f,8.0f));
			amty  = Random.Range(Random.Range(4.0f,7.0f),-Random.Range(4.0f,8.0f));
			this_transform = transform;	
		}
		
		void Update () 
		{
			this_transform.Rotate(Time.deltaTime *amtx, Time.deltaTime *amty, Time.deltaTime *amtz);
		}
		
	}
}

