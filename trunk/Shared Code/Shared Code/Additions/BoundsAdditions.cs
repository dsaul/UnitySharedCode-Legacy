using UnityEngine;
using System.Collections;

namespace OmniLibrary
{
	public static class BoundsAdditions
	{
		/*
		 *      B+---+C
		 *      /   /|
		 *     /   / |
		 *   A+---+D |
		 *   | F+|--+G
		 *   | / | /
		 *   |/  |/ 
		 *  E+---+H
		 * */
		
		public static void BoundingBox(this Bounds bounds, out Vector3 a, out Vector3 b, out Vector3 c,
			out Vector3 d, out Vector3 e, out Vector3 f, out Vector3 g, out Vector3 h)
		{
			a = new Vector3 (bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y, bounds.center.z - bounds.extents.z);
			b = new Vector3 (bounds.center.x - bounds.extents.x, bounds.center.y + bounds.extents.y, bounds.center.z - bounds.extents.z);
			c = new Vector3 (bounds.center.x - bounds.extents.x, bounds.center.y + bounds.extents.y, bounds.center.z + bounds.extents.z);
			d = new Vector3 (bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y, bounds.center.z + bounds.extents.z);
			e = new Vector3 (bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.z - bounds.extents.z);
			f = new Vector3 (bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.z - bounds.extents.z);
			g = new Vector3 (bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.z + bounds.extents.z);
			h = new Vector3 (bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.z + bounds.extents.z);
		}
	}
}