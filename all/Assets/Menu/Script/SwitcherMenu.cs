using UnityEngine;
using System.Collections;

public class SwitcherMenu : MonoBehaviour {

	public GameObject _menuPlane;
	private bool pauseToggle = false;

	// Update is called once per frame
	void OnMouseDown  () {
		ActionThisScript();
	}

	void Update () {
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if(pauseToggle) {
				Time.timeScale = 1;
				ActionThisScript();
			} else {
				//Time.timeScale = 0;
				//ActionThisScript();
				pauseToggle = !pauseToggle;
			}
		}
	}

	public void ActionThisScript() {
		if (Time.timeScale != 0) {
			Debug.Log ("!");
			Time.timeScale = 0;
			_menuPlane.gameObject.GetComponent<MeshRenderer>().enabled = true;
			_menuPlane.gameObject.transform.localPosition = new Vector3 (-0.01f, _menuPlane.gameObject.transform.localPosition.y, _menuPlane.gameObject.transform.localPosition.z);
			gameObject.GetComponent<MeshRenderer>().enabled = false;
			
			object[] allGameButtons = GameObject.FindGameObjectsWithTag("Button");
			
			foreach(GameObject thisButton in allGameButtons) {
				thisButton.gameObject.GetComponent<MeshRenderer>().enabled = false;
			}
		} else {
			Time.timeScale = 1;
			_menuPlane.gameObject.GetComponent<MeshRenderer>().enabled = false;
			_menuPlane.gameObject.transform.localPosition = new Vector3 (-10.01f, _menuPlane.gameObject.transform.localPosition.y, _menuPlane.gameObject.transform.localPosition.z);
			gameObject.GetComponent<MeshRenderer>().enabled = true;
			
			object[] allGameButtons = GameObject.FindGameObjectsWithTag("Button");
			
			foreach(GameObject thisButton in allGameButtons) {
				thisButton.gameObject.GetComponent<MeshRenderer>().enabled = true;
			}
		}
	}
}
