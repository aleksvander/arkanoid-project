using UnityEngine;
using System.Collections;

//public class AlignmentGUI : MonoBehaviour {
public static class AlignmentGUI
	{
		static Vector2 ratioValue;
		
		public static void ThisObjectCalculate(GameObject GO, float screenWidth, float screenHeight)
		{
			ratioValue.x = screenWidth / Screen.width;
			ratioValue.y = screenHeight / Screen.height;
			if (GO.guiTexture != null)
			{
				GO.guiTexture.pixelInset = new Rect(GO.guiTexture.pixelInset.x / ratioValue.x, GO.guiTexture.pixelInset.y / ratioValue.y, GO.guiTexture.pixelInset.width / ratioValue.x, GO.guiTexture.pixelInset.height / ratioValue.y);
			}
			if (GO.guiText != null)
			{
				GO.guiText.pixelOffset = new Vector2(GO.guiText.pixelOffset.x / ratioValue.x, GO.guiText.pixelOffset.y / ratioValue.y);
				GO.guiText.fontSize = (int)(GO.guiText.fontSize / ratioValue.y);
			}
		}
		
		public static void AllObjectsCalculate(float screenWidth, float screenHeight)
		{
			ratioValue.x = screenWidth / Screen.width;
			ratioValue.y = screenHeight / Screen.height;
			GUITexture[] masTex = Object.FindObjectsOfType(typeof(GUITexture)) as GUITexture[];
			GUIText[] masText = Object.FindObjectsOfType(typeof(GUIText)) as GUIText[];
			foreach (GUITexture temp in masTex)
			{
				temp.pixelInset = new Rect(temp.pixelInset.x / ratioValue.x, temp.pixelInset.y / ratioValue.y, temp.pixelInset.width / ratioValue.x, temp.pixelInset.height / ratioValue.y);
			}
			foreach (GUIText temp in masText)
			{
				temp.pixelOffset = new Vector2(temp.pixelOffset.x / ratioValue.x, temp.pixelOffset.y / ratioValue.y);
				temp.fontSize = (int)(temp.fontSize / ratioValue.y);
			}
		}
	}
