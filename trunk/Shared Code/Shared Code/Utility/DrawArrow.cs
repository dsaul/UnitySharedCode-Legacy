using UnityEngine;
using System.Collections;

namespace SharedCode
{
	public static class DrawArrow
	{
		public static void ForGizmo(Vector3 pos, Vector3 direction)
		{
			ForGizmo(pos, direction, 0.25f, 20.0f);
		}
		
		public static void ForGizmo(Vector3 pos, Vector3 direction, float arrowHeadLength, float arrowHeadAngle)
		{
			Gizmos.DrawRay(pos, direction);
			
			Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
			Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
			Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
			Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
		}
		
		public static void ForGizmo(Vector3 pos, Vector3 direction, Color color)
		{
			ForGizmo(pos, direction, color, 0.25f, 20.0f);
		}
		
		public static void ForGizmo(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength, float arrowHeadAngle)
		{
			Gizmos.color = color;
			Gizmos.DrawRay(pos, direction);
			
			Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
			Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
			Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
			Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
		}
		public static void ForDebug(Vector3 pos, Vector3 direction)
		{
			ForDebug(pos, direction, 0.25f, 20.0f);
		}
		
		
		public static void ForDebug(Vector3 pos, Vector3 direction, float arrowHeadLength, float arrowHeadAngle)
		{
			Debug.DrawRay(pos, direction);
			
			Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
			Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
			Debug.DrawRay(pos + direction, right * arrowHeadLength);
			Debug.DrawRay(pos + direction, left * arrowHeadLength);
		}
		
		public static void ForDebug(Vector3 pos, Vector3 direction, Color color)
		{
			ForDebug(pos, direction, color, 0.25f, 20.0f);
		}
		
		public static void ForDebug(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength, float arrowHeadAngle)
		{
			Debug.DrawRay(pos, direction, color);
			
			Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
			Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
			Debug.DrawRay(pos + direction, right * arrowHeadLength, color);
			Debug.DrawRay(pos + direction, left * arrowHeadLength, color);
		}
	}
}
