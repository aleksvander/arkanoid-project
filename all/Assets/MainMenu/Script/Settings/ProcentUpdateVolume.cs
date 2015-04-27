using UnityEngine;
using System.Collections;

public class ProcentUpdateVolume : MonoBehaviour {
	
	private float cacheTmp;
	public int sizeVolume;
	public int sizeMusic;

	public GameObject pump;

	private void Start() {
		//Make reference to transform
		LoadSize();
		cacheTmp = pump.gameObject.transform.localPosition.x;
		//Debug.Log (" cacheTmp " + cacheTmp);
		//LoadSize();
	}

	private void ChangeProcent(int tmpI) {
		if (this.gameObject.name == "VolumeLabel") {
			this.gameObject.GetComponent<GUIText>().text = "Volume " + tmpI + "%";
			ScenesInformation.MusicLevel = sizeVolume = tmpI;
		}
		if (this.gameObject.name == "MusicLabel") {
			this.gameObject.GetComponent<GUIText>().text = "Music " + tmpI + "%";
			ScenesInformation.MusicLevel = sizeMusic = tmpI;
		}
	}

	//call method
	public void TextUpdate() {
		//- 0.45 to 0.35
		//Debug.Log ("cache tmp " + cacheTmp + " transform.parent.localPosition.x " + transform.parent.localPosition.x);
		if (cacheTmp != transform.parent.localPosition.x) {
			cacheTmp = pump.gameObject.transform.localPosition.x;
			ChangeProcent(ProcentPumpInfo.CalculateProcent(cacheTmp));
			SaveResult();
		}
		float tmpF = ProcentPumpInfo.CalculateProcent (cacheTmp);
		//Debug.Log (" tmpF " + tmpF);
	}

	private void LoadSize() {
		if (this.gameObject.name == "VolumeLabel") {
			sizeVolume = PlayerPrefs.GetInt("saveVolume");
			ScenesInformation.VolumeLevel = sizeVolume;
			ChangeProcent(sizeVolume);
			transform.parent.gameObject.GetComponent<ControlPumpVolume>().SendToTransformPump(ProcentPumpInfo.DeCalculateProcent(sizeVolume));
			if (!PlayerPrefs.HasKey("saveVolume")) {
				PlayerPrefs.SetInt("saveVolume", 100);
			}
		}
		if (this.gameObject.name == "MusicLabel") {
			sizeMusic = PlayerPrefs.GetInt("saveMusic");
			ScenesInformation.MusicLevel = sizeMusic;
			ChangeProcent(sizeMusic);
			transform.parent.gameObject.GetComponent<ControlPumpVolume>().SendToTransformPump(sizeMusic);
			transform.parent.gameObject.GetComponent<ControlPumpVolume>().SendToTransformPump(ProcentPumpInfo.DeCalculateProcent(sizeMusic));
			if (!PlayerPrefs.HasKey("saveMusic")) {
				PlayerPrefs.SetInt("saveMusic", 100);
			}
		}

	}

	private void SaveResult() {
		//Debug.Log (" this.gameObject.name " + this.gameObject.name);
		if (this.gameObject.name == "VolumeLabel") {
			//Debug.Log(" sizeVolume " + sizeVolume);
			PlayerPrefs.SetInt("saveVolume", sizeVolume);
			PlayerPrefs.Save();
		}
		if (this.gameObject.name == "MusicLabel") {
			//Debug.Log(" sizeMusic " + sizeMusic);
			PlayerPrefs.SetInt("saveMusic", sizeMusic);
			PlayerPrefs.Save();		
		}

		//Debug.Log ("SAVE MUSIC" + sizeVolume + " ");
	}
}
