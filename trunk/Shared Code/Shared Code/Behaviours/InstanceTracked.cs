using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SharedCode.Behaviours
{
	//[AdvancedInspector]
	public abstract class InstanceTracked<T> : SharedCode.Behaviours.Base where T : InstanceTracked<T>
	{
		static List<T> c_Instances = null;
		public static List<T> Instances
		{
			get
			{
				if (null == c_Instances)
					c_Instances = new List<T>();
				return c_Instances;
			}
		}

		public static int InstanceCount
		{
			get
			{
				if (null == c_Instances)
					return 0;
				return c_Instances.Count;
			}
		}

		public static T Random
		{
			get
			{
				if (0 == c_Instances.Count)
					return default(T);

				return c_Instances[UnityEngine.Random.Range(0, c_Instances.Count)];
			}
		}

		public static T First
		{
			get
			{
				if (null == c_Instances)
					return default(T);
				if (0 == c_Instances.Count)
					return default(T);
				return c_Instances[0];
			}
		}

		public static T Main
		{
			get
			{
				return First;
			}
		}

		public static T GetFromUniqueName(string uniqueName)
		{
			if (string.IsNullOrEmpty(uniqueName))
				return default(T);
			if (null == c_Instances)
				return default(T);
			for (int i = 0; i < c_Instances.Count; i++)
			{
				if (c_Instances[i].UniqueName == uniqueName)
					return c_Instances[i];
			}
			return default(T);
		}


		private static Action<T> _OnInstanceAdded; // T ship
		public static Action<T> OnInstanceAdded 
		{
			get
			{
				if (null == _OnInstanceAdded)
				{
					_OnInstanceAdded = new Action<T>(delegate(T obj) {
						Instances.Add(obj);
					});
				}
				return _OnInstanceAdded;
			}
			set
			{
				_OnInstanceAdded = value;
			}
		}

		public delegate void InstanceRemoved(T o);
		private static InstanceRemoved _OnInstanceRemoved;
		public static InstanceRemoved OnInstanceRemoved
		{
			get
			{
				if (null == _OnInstanceRemoved)
				{
					_OnInstanceRemoved = new InstanceRemoved(delegate(T obj) {
						Instances.Remove(obj);
					});
				}
				return _OnInstanceRemoved;
			}
			set
			{
				_OnInstanceRemoved = value;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (string.IsNullOrEmpty(m_UniqueName))
			{
				m_UniqueName = System.Guid.NewGuid().ToString();
			}
			OnInstanceAdded((T)this);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			OnInstanceRemoved((T)this);
		}
		
		//[Inspect]
		[SerializeField]
		string m_UniqueName = "";

		public virtual string UniqueName
		{
			get { return m_UniqueName; }
			set { m_UniqueName = value; }
		}


	}





















































}