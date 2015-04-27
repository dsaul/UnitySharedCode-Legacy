using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SharedCode
{
	public static class LineRendererAdditions
	{
		public static void SetVertices(this LineRenderer lr, params Vector3[] list)
		{
			lr.SetVertexCount(list.Length);
			for (int i=0; i<list.Length; i++){
				lr.SetPosition(i,list[i]);
			}
		}
		
		public static void SetVertices(this LineRenderer lr, List<Vector3> list)
		{
			lr.SetVertexCount(list.Count);
			for (int i=0; i<list.Count; i++){
				lr.SetPosition(i,list[i]);
			}
		}
	}
}