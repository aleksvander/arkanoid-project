using UnityEngine;
using System.Collections;

public class ScenesInformation : MonoBehaviour {

	public static int CountScenesG;
	public static int CountScenesP;
	public static int CurrentLevel;
	public static int VolumeLevel;
	public static int MusicLevel;
	public static bool TypeControl;
	public static int PowerInt = 0;
	public static int ShieldInt = 0;

	public int _CountScenesG;
	public int _CountScenesP;

	void Awake() {
		LoadSaveInformation ();
	}

	void Start() {
		CountScenesG = _CountScenesG;
		CountScenesP = _CountScenesP;
	}

	private void LoadSaveInformation() {
		ScenesInformation.PowerInt = PlayerPrefs.GetInt ("Power");
		//Debug.Log ("AWAKE" + ScenesInformation.PowerInt);
	}
}
