using UnityEngine;
using System.Collections;

namespace SharedCode
{
	[RequireComponent(typeof(MeshRenderer))]
	public class DrawBoundsGizmo : MonoBehaviour
	{
		void OnDrawGizmos()
		{
			Gizmos.color = Color.blue;
			
			MeshRenderer ren = GetComponent<MeshRenderer>();
			Bounds bounds = ren.bounds;
			
			Vector3 pt1 = new Vector3(bounds.min.x,0,bounds.min.z);
			Vector3 pt2 = new Vector3(bounds.min.x,0,bounds.max.z);
			Vector3 pt3 = new Vector3(bounds.max.x,0,bounds.max.z);
			Vector3 pt4 = new Vector3(bounds.max.x,0,bounds.min.z);
			
			Gizmos.DrawLine(pt1,pt2);
			Gizmos.DrawLine(pt2,pt3);
			Gizmos.DrawLine(pt3,pt4);
			Gizmos.DrawLine(pt4,pt1);
		}
	}
	
}
