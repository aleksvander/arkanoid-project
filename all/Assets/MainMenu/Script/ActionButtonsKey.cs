using UnityEngine;
using System.Collections;

public class ActionButtonsKey : MonoBehaviour {

	public GameObject LabelLevelNumber;
	public GameObject SettingsMenu;

	delegate void DelegateSelectMethod(Transform _go);

	private bool[] LoadListG;
	private bool[] LoadListPreG;

	// Update is called once per frame
	public void SelectAction (string _name) {
		Debug.Log (" INPUT NAME : " + _name);
		switch (_name) {
		case "Play":
			PlayAction();
			break;
		case "ReturnMM":
			ReturnMM();
			break;
		case "SelectLevel":
			SelectLevelG();
			break;
		case "ReturnLS":
			PlayAction();
			break;
		case "Setting":
			SettingActive();
			break;
		case "Shop":
			ShopActive();
			break;
		case "Score":
			ScoreActive();
			break;
		default:

			break;
		}
	}

	private void PlayAction() {
		SelectSkinTexture (1);
		AllChildAndAction (ActivateObject, "");
		AllChildAndAction (DeactivateObject, "Buttons for GeneralSelectLevel");
		LabelLevelNumber.gameObject.GetComponent<GUIText>().enabled = true;
	}

	private void ReturnMM() {
		SelectSkinTexture (0);
		AllChildAndAction (ActivateObject, "");
		AllChildAndAction (DeactivateObject, "Buttons for MainMenu");
		LabelLevelNumber.gameObject.GetComponent<GUIText>().enabled = false;
	}

	private void SelectLevelG() {
		SelectSkinTexture (2);
		AllChildAndAction (ActivateObject, "");
		AllChildAndAction (DeactivateObject, "Buttons for ListSelectLevel");
		LabelLevelNumber.gameObject.GetComponent<GUIText>().enabled = false;
		AllChildChildAndAction (SetStatus, "Buttons for ListSelectLevel");
	}

	private void ShopActive() {
		SelectSkinTexture (3);
		AllChildAndAction (ActivateObject, "");
		AllChildAndAction (DeactivateObject, "Shop");
		LabelLevelNumber.gameObject.GetComponent<GUIText>().enabled = false;
	}

	private void ScoreActive() {
		SelectSkinTexture (4);
		AllChildAndAction (ActivateObject, "");
		AllChildAndAction (DeactivateObject, "Score");
		LabelLevelNumber.gameObject.GetComponent<GUIText>().enabled = false;
	}

	private void SelectSkinTexture(int i) {
		this.gameObject.GetComponent<ReSelectTexture> ().SelectTexture (i);
	}

	//foreach all child and select method
	private void AllChildAndAction(DelegateSelectMethod _selectMethod, string _expName) {
		foreach (Transform child in transform) {
			if (_expName == "") {
				_selectMethod(child);
			} else if (_expName != child.name) {
				_selectMethod(child);
			}
		}
	}

	private void AllChildChildAndAction(DelegateSelectMethod _selectMethod, string _expName) {
		LoadListG = SaveLoadDataMenu.LoadS (ScenesInformation.CurrentLevel);

		foreach (Transform child in transform) {
			if (_expName == child.name) {
				foreach (Transform childChild in child) {
					_selectMethod(childChild);
				}
			}
		}
	}

	//deactivate all object
	private void DeactivateObject(Transform _nameGo) {
		_nameGo.gameObject.SetActiveRecursively (false);
	}

	//activate all object
	private void ActivateObject(Transform _nameGo) {
		_nameGo.gameObject.SetActiveRecursively (true);
	}

	//SetStatusButtonsListLevel
	private void SetStatus(Transform _nameGo) {

		bool _statusPre;
		
		if (ScenesInformation.CurrentLevel == 1) {
			_statusPre = true;
		} else {
			_statusPre = CalculatePreComplete();
		}

		for (int i = 0; i < LoadListG.Length; i++) {
			if (i + "" == _nameGo.name) {
				if (i > 0) {
					ChangeStatus(_nameGo, i, LoadListG[i], _statusPre, LoadListG[i - 1]);
				} else {
					ChangeStatus(_nameGo, i, LoadListG[i], _statusPre, LoadListG[i]);
				}
			}
		}
	}

	//calculatePreComplete
	private bool CalculatePreComplete() {
		LoadListPreG = SaveLoadDataMenu.LoadS (ScenesInformation.CurrentLevel - 1);
		int count = 0;
		for (int i = 0; i < LoadListPreG.Length; i++) {
			if (LoadListPreG[i]) count++;
		}
		if (count > 12) {
			return true;
		}
		return false;
	}

	//ChangeStatus
	private void ChangeStatus(Transform _nameGo, int num, bool _status, bool _status2, bool _status3) {
		//Debug.Log (" _status2 " + _status2 + " _nameGo " + _nameGo + " num " + num + " _status " + _status + " _status3 " + _status3);
		bool _block = false;
		if (_status2) {
			if (!_status) {
				if (num > 1) {
					_nameGo.gameObject.GetComponent<ReSelectTexture>().SelectTexture(2);
					_block = true;
				} else {
					_nameGo.gameObject.GetComponent<ReSelectTexture>().SelectTexture(0);
					_block = false;
				}
			} else {
				_nameGo.gameObject.GetComponent<ReSelectTexture>().SelectTexture(1);
				_block = false;
			}
		} else {
			if (!_status) {
				_nameGo.gameObject.GetComponent<ReSelectTexture>().SelectTexture(2);
				_block = true;
			} else {
				_nameGo.gameObject.GetComponent<ReSelectTexture>().SelectTexture(1);
				_block = true;
			}
		}

		if (_status3) {
			_nameGo.gameObject.GetComponent<ReSelectTexture>().SelectTexture(0);
			_block = false;
		}

		if (_block) {
			_nameGo.gameObject.GetComponent<Collider>().enabled = false;
		} else {
			_nameGo.gameObject.GetComponent<Collider>().enabled = true;
		}
	}

	private void SettingActive() {
		SettingsMenu.gameObject.transform.localPosition = new Vector3 (SettingsMenu.gameObject.transform.localPosition.x + 10, SettingsMenu.gameObject.transform.localPosition.y, SettingsMenu.gameObject.transform.localPosition.z);
		this.gameObject.SetActiveRecursively (false);
	}
}
