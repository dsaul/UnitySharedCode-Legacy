﻿using UnityEngine;
using System.Collections;

namespace OmniLibrary
{
	public static class RendererAdditions
	{
		public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
		{
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
			return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
		}



	}
}