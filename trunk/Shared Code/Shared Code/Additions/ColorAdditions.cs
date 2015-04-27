using UnityEngine;
using System.Collections;

namespace OmniLibrary
{
	public static class ColorAdditionsAdditions
	{
		public static Color MakeRandomColor(this Color color, float minClamp = 0.5f)
		{
			var randCol = UnityEngine.Random.onUnitSphere * 3;
			randCol.x = Mathf.Clamp(randCol.x, minClamp, 1f);
			randCol.y = Mathf.Clamp(randCol.y, minClamp, 1f);
			randCol.z = Mathf.Clamp(randCol.z, minClamp, 1f);
			
			return new Color(randCol.x, randCol.y, randCol.z, 1f);
		}
	}
}