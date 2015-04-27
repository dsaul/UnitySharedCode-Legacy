using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace SharedCode
{
	public static class MoveUnityWindow
	{
		#if UNITY_STANDALONE_WIN || UNITY_EDITOR
		[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
		private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
		[DllImport("user32.dll", EntryPoint = "FindWindow")]
		public static extern IntPtr FindWindow(System.String className, System.String windowName);
		
		public static void SetPosition(string windowTitle, int x, int y, int resX = 0, int resY = 0) {
			SetWindowPos(FindWindow(null, windowTitle), 0, x, y, resX, resY, resX * resY == 0 ? 1 : 0);
		}
		#endif
	}
}