﻿using UnityEngine;
using System.Collections;

namespace OmniLibrary
{
	public static class MaterialAdditions
	{
		public static void SetAlpha (this Material material, float value)
		{
			Color color = material.color;
			color.a = value;
			material.color = color;
		}
	}
}