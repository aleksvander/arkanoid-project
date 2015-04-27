using UnityEngine;
using System.Collections;

public class Close : MonoBehaviour {

	public GameObject mainmenu;
	public GameObject podmainmenu;

	public void OnMouseUp() {
		CloseSettings();
	}

	private void CloseSettings() {
		if (Application.loadedLevelName == "MainMenu") {
			transform.parent.transform.localPosition = new Vector3 (transform.parent.transform.localPosition.x - 10, transform.parent.transform.localPosition.y, transform.parent.transform.localPosition.z);
			mainmenu.gameObject.SetActive (true);
			podmainmenu.gameObject.SetActiveRecursively (true);
		} else {

		}
	}
}
