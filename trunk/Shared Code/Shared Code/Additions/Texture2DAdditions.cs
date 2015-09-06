using UnityEngine;
using System.Collections;

namespace SharedCode
{
	public static class Texture2DAdditions
	{
		public static Texture2D CreateMirroredTexture(this Texture2D originalTexture, bool horizontal, bool vertical)
		{
			Texture2D newTexture = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGBA32, false);
			Color32[] originalPixels = originalTexture.GetPixels32(0);
			Color32[] newPixels = newTexture.GetPixels32(0);
			for (int y = 0; y < originalTexture.height; y++)
			{
				for (int x = 0; x < originalTexture.width; x++)
				{
					int newX = horizontal ? (newTexture.width - 1 - x) : x;
					int newY = vertical ? (newTexture.height - 1 - y) : y;
					newPixels[(newY * newTexture.width) + newX] = originalPixels[(y * originalTexture.width) + x];
				}
			}
			newTexture.SetPixels32(newPixels, 0);
			newTexture.Apply();
			return newTexture;
		}
	}
}
