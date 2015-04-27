using UnityEngine;
using System.Collections;

public class SelectLoadLevel : MonoBehaviour {

	void OnMouseUp() {
		//Debug.Log (" CountScenesG " + ScenesInformation.CurrentLevel + " this.gameObject.name " + this.gameObject.name);
		int num = int.Parse(this.gameObject.name) + 1;
		Application.LoadLevel ("game " + ScenesInformation.CurrentLevel + "-" + num);
	}
}
