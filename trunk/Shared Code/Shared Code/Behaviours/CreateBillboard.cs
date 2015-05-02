using UnityEngine;
using System.Collections;

namespace SharedCode.Behaviours
{
	/* 
Make a billboard out of an object in the scene 
The camera will auto-place to get the best view of the object so no need for camera adjustment 

To use - place an object in an empty scene with just camera and any lighting you want. 
Add this script to your scene camera and link to the object you want to render. 
Press play and you will get a snapshot of the object (looking down the +Z-axis at it) saved out to billboard.png in your project folder 
Any pixels colored the same as the camera background color will be transparent 
*/ 
#if !UNITY_WEBPLAYER
	public class CreateBillboard : MonoBehaviour
	{
		GameObject objectToRender = null; 
		int imageWidth = 128; 
		int imageHeight = 128; 
		
		IEnumerator Start() 
		{ 
			if (!objectToRender) yield return null; 
			
			//grab the main camera and mess with it for rendering the object - make sure orthographic
			Camera cam = Camera.main; 
			cam.orthographic = true; 
			
			//render to screen rect area equal to out image size 
			float rw = imageWidth; rw /= Screen.width; 
			float rh = imageHeight; rh /= Screen.height; 
			cam.rect = new Rect(0,0,rw,rh); 
			
			//grab size of object to render - place/size camera to fit 
			Bounds bb = objectToRender.GetComponent<Renderer>().bounds; 
			
			//place camera looking at centre of object - and backwards down the z-axis from it
			cam.transform.position = bb.center; 
			Vector3 camPosition = cam.transform.position;
			camPosition.z = -1.0f + (bb.min.z * 2.0f); 
			cam.transform.position = camPosition;
			
			//make clip planes fairly optimal and enclose whole mesh 
			cam.nearClipPlane = 0.5f; 
			cam.farClipPlane = -cam.transform.position.z + 10.0f + bb.max.z; 
			//set camera size to just cover entire mesh 
			cam.orthographicSize = 1.01f * Mathf.Max( (bb.max.y - bb.min.y)/2.0f, (bb.max.x - bb.min.x)/2.0f);
			
			camPosition = cam.transform.position;
			camPosition.y += cam.orthographicSize * 0.05f; 
			cam.transform.position = camPosition;
			
			//render 
			yield return new WaitForEndOfFrame(); 
			
			var tex = new Texture2D( imageWidth, imageHeight, TextureFormat.ARGB32, false ); 
			// Read screen contents into the texture 
			tex.ReadPixels(new Rect(0, 0, imageWidth, imageHeight), 0, 0 ); 
			tex.Apply(); 
			
			//turn all pixels == background-color to transparent 
			Color bCol = cam.backgroundColor; 
			var alpha = bCol; 
			alpha.a = 0.0f; 
			for(int y = 0; y < imageHeight; y++) 
			{ 
				for(int x = 0; x < imageWidth; x++) 
				{ 
					Color c = tex.GetPixel(x,y); 
					if (c.r == bCol.r) 
						tex.SetPixel(x,y,alpha); 
				} 
			} 
			tex.Apply(); 
			
			// Encode texture into PNG 
			var bytes = tex.EncodeToPNG(); 
			Destroy( tex ); 
			System.IO.File.WriteAllBytes(Application.dataPath + "/../billboard.png", bytes); 
		}  
	}
#endif
}

