using UnityEngine;
using System.Collections;

public class goMenu : MonoBehaviour {

	public string scena = "MainMenu";
	public static string generalNumberLevelStr;
	public static bool go_to_level_select;

	private int gNumber;
	private int pNumber;

	void Start() {
		gNumber = UpdateLevelNumberG.GetLevel_number_g;
		pNumber = UpdateLevelNumberG.GetLevel_number_p - 1;
	}

	// Update is called once per frame
	void OnMouseUp () {
		if (gameObject.name == "LEVELSELECT") {
			if (EndGame.GLOBAL_STATUS) {
				UpdateLevelCompleteResult();
			}
			go_to_level_select = true;
			generalNumberLevelStr = Application.loadedLevelName;
			Resume ();
			Application.LoadLevel(scena);
		} else {
			go_to_level_select = false;
		}
		if (gameObject.name == "MAINMENU") {
			//Debug.Log("GO MAIN MENU");
			if (EndGame.GLOBAL_STATUS) {
				UpdateLevelCompleteResult();
			}
			Resume();
			Application.LoadLevel(scena);
		}
		if (gameObject.name == "RESUME") {
			if (EndGame.GLOBAL_STATUS) {
				UpdateLevelCompleteResult();
			}
			Resume();
		}
		if (gameObject.name == "RESTART") {
			if (EndGame.GLOBAL_STATUS) {
				UpdateLevelCompleteResult();
			}
			Time.timeScale = 1;
			//Debug.Log(gameObject.transform.parent.name);
			if (gameObject.transform.parent.name == "WinLose") {
				transform.position = new Vector3(10f, transform.position.y, transform.position.z);
				gameObject.GetComponent<MeshRenderer>().enabled = false;
				EndGame.allNull();
			}
			Application.LoadLevel(Application.loadedLevelName); 
		}
		if (gameObject.name == "NEXTLEVEL") {
			Time.timeScale = 1;
			if (EndGame.GLOBAL_STATUS) {
				//if ((Application.loadedLevelName).Length == 7) Application.LoadLevel((Application.loadedLevelName).Substring(4,1) + (Application.loadedLevelName).Substring(6,1));
				//if ((Application.loadedLevelName).Length == 8) Application.LoadLevel((Application.loadedLevelName).Substring(4,1) + (Application.loadedLevelName).Substring(6,2));
				UpdateLevelCompleteResult();
				if (pNumber != 20) {
					string scenaTmp = "game" + gNumber + "-" + pNumber + 1;
					Application.LoadLevel(scenaTmp);
				} else {
					string scenaTmp = "game" + gNumber + 1 + "-" + 1;
					Application.LoadLevel(scenaTmp);
				}
			} else {
				go_to_level_select = true;
				generalNumberLevelStr = Application.loadedLevelName;
				Resume ();
				Application.LoadLevel(scena);
			}
		}
	}

	private void UpdateLevelCompleteResult() {
		SaveLoadDataMenu.Calculate(gNumber, pNumber, true);
	}

	private void Resume() {
		TransformPositionHideUnHide(-10.01f);
		gameObject.GetComponent<MeshRenderer>().enabled = false;
		Time.timeScale = 1;
		object[] allGameButtons = GameObject.FindGameObjectsWithTag("Button");
		
		foreach(GameObject thisButton in allGameButtons) {
			thisButton.gameObject.GetComponent<MeshRenderer>().enabled = true;
		}
	}

	public void TransformPositionHideUnHide(float _shag) {
		transform.parent.localPosition = new Vector3 (_shag, transform.parent.localPosition.y, transform.parent.localPosition.z);
	}
}
