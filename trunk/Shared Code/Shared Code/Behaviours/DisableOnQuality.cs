using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SharedCode
{
	public class DisableOnQuality : MonoBehaviour
	{
		public string quality = "Fastest";
		
		void OnEnable()
		{
			RunCheck();
		}
		
		public void RunCheck()
		{
			string[] names = QualitySettings.names;
			int index = Array.IndexOf<string>(names,quality);
			
			if (QualitySettings.GetQualityLevel() == index) {
				gameObject.SetActive(false);
			} else {
				gameObject.SetActive(true);
			}
		}
		
		#region Object Tracking
		
		private static List<DisableOnQuality> _active = null;
		
		public static List<DisableOnQuality> Active {
			get {
				if (null == _active) _active = new List<DisableOnQuality>();
				return _active;
			}
		}
		
		public static IEnumerable<DisableOnQuality> EnumerateActive()
		{
			using (IEnumerator<DisableOnQuality> enumerator = Active.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					yield return enumerator.Current;
				}
			}
		}
		
		public static void RunCheckOnAll()
		{
			using (IEnumerator<DisableOnQuality> enumerator = Active.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					enumerator.Current.RunCheck();
				}
			}
		}
		
		void Awake()
		{
			Active.Add(this);
		}
		
		void OnDestroy()
		{
			Active.Remove(this);
		}
		
		#endregion Object Tracking
	}
	
	
	
}
