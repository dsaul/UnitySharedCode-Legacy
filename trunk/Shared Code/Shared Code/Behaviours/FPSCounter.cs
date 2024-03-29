﻿using UnityEngine;
using System.Collections;

namespace SharedCode.Behaviours
{
	[RequireComponent(typeof(GUIText))]
	public class FPSCounter : MonoBehaviour
	{

		public float frequency = 0.5f;


		public int FramesPerSec { get; protected set; }

		private void Start()
		{
			StartCoroutine(FPS());
		}

		private IEnumerator FPS()
		{
			for (; ; )
			{
				// Capture frame-per-second
				int lastFrameCount = Time.frameCount;
				float lastTime = Time.realtimeSinceStartup;
				yield return new WaitForSeconds(frequency);
				float timeSpan = Time.realtimeSinceStartup - lastTime;
				int frameCount = Time.frameCount - lastFrameCount;

				// Display it
				FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
				gameObject.GetComponent<GUIText>().text = FramesPerSec.ToString() + " fps";
			}
		}
	}
}
