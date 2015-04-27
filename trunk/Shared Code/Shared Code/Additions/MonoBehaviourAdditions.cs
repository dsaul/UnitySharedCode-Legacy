using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SharedCode
{
	public static class MonoBehaviourAdditions
	{
		#region Bounds
		
		public static Bounds? GetCompleteBounds(this MonoBehaviour mb, bool ignoreParticleSystems, bool ignoreTriggers, string[] ignoreTags)
		{
			Renderer renderer1 = mb.GetComponentInChildren<Renderer>();
			if (null == renderer1) return null;
			
			Bounds bounds = renderer1.bounds;
			
			Renderer[] subRenderers = mb.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < subRenderers.Length; i++)
			{
				Renderer subRenderer = subRenderers[i];
				if (0 != Array.FindAll(ignoreTags, s => s.Equals(subRenderer.gameObject.tag)).Length)
					continue;
				if (true == ignoreParticleSystems && null != subRenderer.GetComponent<ParticleSystem>())
					continue;
				if (true == ignoreTriggers && null != subRenderer.GetComponent<Collider>() && true == subRenderer.GetComponent<Collider>().isTrigger)
					continue;

				//if (subRenderer.gameObject.tag == Const.Tags.Ignore) continue;
				if (null != subRenderer && subRenderer != renderer1)
				{
					bounds.Encapsulate(subRenderer.bounds);
				}
			}
			return bounds;
		}
		
		public static Bounds? GetCompleteBounds(this MonoBehaviour mb, bool ignoreParticleSystems, bool ignoreTriggers)
		{
			return GetCompleteBounds(mb,ignoreParticleSystems,ignoreTriggers, new string[] {"Ignore!"});
		}
		
		public static Bounds? GetCompleteBounds(this MonoBehaviour mb)
		{
			return GetCompleteBounds(mb,true,true,new string[] {"Ignore!"});
		}
		
		#endregion Bounds
	}




















































}
