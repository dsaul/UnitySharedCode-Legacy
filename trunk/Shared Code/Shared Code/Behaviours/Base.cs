using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SharedCode.Behaviours
{
	public abstract class Base : SharedCode.Behaviours.Internal.TimerBehaviour
	{
		#region Delegates & Types

		public Action<Base> OnFinishedSpawnSetup; // OLMonoBehaviour source

		public class SignalSkeleton
		{
			public Func<bool> SubscribeCallback { get; set; } // returns success
			public Func<bool> UnSubscribeCallback { get; set; } // returns success
		}

		Queue<SignalSkeleton> m_SignalsSubscribed;
		Queue<SignalSkeleton> m_SignalsNonSubscribed;
		Queue<SignalSkeleton> m_SignalsFailed;

		protected Queue<Action> ExecuteOnMainThread { get; set; }

		#endregion Delegates & Types
		#region Events

		protected virtual void Awake()
		{
			OnFinishedSpawnSetup = new Action<Base>(delegate { });
			m_SignalsSubscribed = new Queue<SignalSkeleton>();
			m_SignalsNonSubscribed = new Queue<SignalSkeleton>(DefaultSignals);
			m_SignalsFailed = new Queue<SignalSkeleton>();
			ExecuteOnMainThread = new Queue<Action>();

        }

		protected virtual void Start()
		{
			OnSubscribeToSignals();
        }

		protected virtual void FixedUpdate()
		{

		}

		protected override void Update()
		{
			base.Update();

			while (ExecuteOnMainThread.Count > 0)
			{
				ExecuteOnMainThread.Dequeue().Invoke();
			}
		}

		protected virtual void OnEnable()
		{
			OnSubscribeToSignals();
        }

		protected virtual void OnDisable()
		{
			OnUnsubscribeToSignals();
        }

		protected override void OnDestroy()
		{
			base.OnDestroy();

			OnUnsubscribeToSignals();
        }

		#endregion Events
		#region Signals

		protected abstract List<SignalSkeleton> DefaultSignals
		{
			get;
		}

		

		void OnSubscribeToSignals()
		{
			while (m_SignalsNonSubscribed.Count > 0)
			{
				SignalSkeleton skel = m_SignalsNonSubscribed.Dequeue();

				Assert.IsNotNull<Func<bool>>(skel.SubscribeCallback);
				Assert.IsNotNull<Func<bool>>(skel.UnSubscribeCallback);

				if (true == skel.SubscribeCallback())
					m_SignalsSubscribed.Enqueue(skel);
				else
					m_SignalsFailed.Enqueue(skel);
            }


		}

		void OnUnsubscribeToSignals()
		{
			while (m_SignalsSubscribed.Count > 1)
			{
				SignalSkeleton skel = m_SignalsSubscribed.Dequeue();

				Assert.IsNotNull<Func<bool>>(skel.SubscribeCallback);
				Assert.IsNotNull<Func<bool>>(skel.UnSubscribeCallback);

				if (true == skel.UnSubscribeCallback())
					m_SignalsNonSubscribed.Enqueue(skel);
				else
					m_SignalsSubscribed.Enqueue(skel);
			}
		}

		

		#endregion Signals
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