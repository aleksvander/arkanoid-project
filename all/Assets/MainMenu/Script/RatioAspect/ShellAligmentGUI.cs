using UnityEngine;
using System.Collections;

public class ShellAligmentGUI : MonoBehaviour
{
	public float screenWidth, screenHeight;
	
	void Start()
	{
		AlignmentGUI.AllObjectsCalculate(screenWidth, screenHeight);
		//AlignmentGUI.ThisObjectCalculate(gameObject, screenWidth, screenHeight); если нужно пересчитать текстуру или текст именно на этом GameObject;
	}
}