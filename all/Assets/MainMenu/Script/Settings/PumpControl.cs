using UnityEngine;
using System.Collections;

public class PumpControl : MonoBehaviour {
	
	public GameObject pumpAcc;
	public GameObject pumbSens;
	private bool typeControl;

	void Start() {
		//Debug.Log (PlayerPrefsX.GetBool("TypeControl"));
		if (PlayerPrefsX.GetBool("TypeControl")) {
			AccOn();
			typeControl = true;
		} else {
			SensOn();
			typeControl = false;
		}
		ScenesInformation.TypeControl = typeControl;
	}

	void OnMouseUp() {
		if (name == "CheckThisControlAcc") {
			AccOn();
			typeControl = true;
		}

		if (name == "CheckThisControlTouch") {
			SensOn();
			typeControl = false;
		}
		ScenesInformation.TypeControl = typeControl;
		SaveTypeControl();
	}

	private void SensOn() {
		if (pumpAcc != null && pumbSens != null) {
			pumpAcc.gameObject.GetComponent<MeshRenderer>().enabled = false;
			pumbSens.gameObject.GetComponent<MeshRenderer>().enabled = true;
		}
	}

	private void AccOn() {
		if (pumpAcc != null && pumbSens != null) {
			pumpAcc.gameObject.GetComponent<MeshRenderer>().enabled = true;
			pumbSens.gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}

	private void SaveTypeControl() {
		PlayerPrefsX.SetBool("TypeControl", typeControl);
		PlayerPrefs.Save();
		ScenesInformation.TypeControl = typeControl;
	}


}
