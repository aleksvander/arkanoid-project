using UnityEngine;
using System.Collections;

public class Speed : MonoBehaviour {

	public void Update() {
		if (guiText.text != "SPEED: " + GlobalSpeed.GetSetGlobalSpeed) guiText.text = "SPEED: " + GlobalSpeed.GetSetGlobalSpeed;
	}


}
