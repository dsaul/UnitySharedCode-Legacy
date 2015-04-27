using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ShinobiTools;

namespace SharedCode
{
	public abstract class OLMonoBehaviour : ShinobiMono
	{
		#region Delegates

		public Action<OLMonoBehaviour> OnFinishedSpawnSetup; // OLMonoBehaviour source

		#endregion Delegates
		#region Events

		protected virtual void Awake()
		{
			OnFinishedSpawnSetup = new Action<OLMonoBehaviour>(delegate { });
		}

		protected virtual void Start()
		{

		}

		protected virtual void FixedUpdate()
		{

		}

		protected override void Update()
		{
			base.Update();
		}

		protected virtual void OnEnable()
		{

		}

		protected virtual void OnDisable()
		{

		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		#endregion Events
		#region Anccessors

		public bool IsVisibleFromCamera(Camera camera)
		{
			Renderer[] subRenderers = GetComponentsInChildren<Renderer>();
			for (int i = 0; i < subRenderers.Length; i++)
			{
				if (subRenderers[i].IsVisibleFrom(camera))
					return true;
			}
			return false;
		}

		#endregion Anccessors
	}



















































}