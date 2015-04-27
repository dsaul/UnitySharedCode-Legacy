﻿using UnityEngine;
using System.Collections;

namespace OmniLibrary
{
	public class ContinuousRotate : MonoBehaviour
	{
		public float speed = 1;
		
		void Update ()
		{
			transform.Rotate(0,speed*Time.deltaTime,0);
		}
	}
	
}