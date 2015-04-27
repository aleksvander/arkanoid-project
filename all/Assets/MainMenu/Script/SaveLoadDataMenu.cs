using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;

public class SaveLoadDataMenu : MonoBehaviour {
	
	private int numScenG;
	private int numScenP;

	public GameObject go;

	// Use this for initialization
	void Start () {
		numScenG = ScenesInformation.CountScenesG;
		numScenP = ScenesInformation.CountScenesP;
		CalculateAllGameScenes();
		//ResetGame (0);
		//LoadforTest(1,1);
	}

	public static void Calculate(int gNumS, int pNumS, bool result) {
		Camera.main.GetComponent<SaveLoadDataMenu>().UpdateSave(gNumS, pNumS, result);
	}

	private void CalculateAllGameScenes() {
		//PlayerPrefs.DeleteKey("CompleteLevel1");
		//PlayerPrefs.SetInt("BeginGame", 0);
		//Debug.Log (PlayerPrefs.HasKey("CompleteLevel1"));
		//Debug.Log (PlayerPrefs.GetInt("BeginGame"));
		if (!PlayerPrefs.HasKey("BeginGame")) {
			ResetGame(0);
		} else {
			if (PlayerPrefs.GetInt("BeginGame") != numScenG) {
				ResetGame(PlayerPrefs.GetInt("BeginGame") - numScenG);
			}
		}
	}

	private void ResetGame(int numCorrect) {
		if (numCorrect == 0) {
			CreateAllSpaces(1);
		} else {
			if (numCorrect < 0) {
				numCorrect *= -1;
				CreateAllSpaces(numCorrect);
			} else if (numCorrect > 0) {
				CreateAllSpaces(numCorrect);
			}
		}
		Debug.Log ("RESET GAME!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
		PlayerPrefs.SetInt("BeginGame", numScenG);
	}

	private void CreateAllSpaces(int numCorrect) {
		int tmpG = numCorrect;
		bool[] arrFalse1 = new bool[numScenP];
		for (int y = 1; y < numScenP; y++) {
			if (y == 1) {
				arrFalse1[y] = true;	
			} else {
				arrFalse1[y] = false;	
			}
		}
		bool[] arrFalse2 = new bool[numScenP];
		for (int y = 1; y < numScenP; y++) {
			arrFalse2[y] = false;	
		}

		for (int i = 1; i < tmpG; i++) {
			if (i == 1) {
				PlayerPrefsX.SetBoolArray("CompleteLevel" + i, arrFalse1);
			} else {
				PlayerPrefsX.SetBoolArray("CompleteLevel" + i, arrFalse2);
			}
			PlayerPrefs.Save();
		}
	}

	//for test
	private void UpdateSave(int gNum, int pNum, bool result) {
		bool[] tmpArr = LoadFromSave(gNum);
		tmpArr[pNum] = result;
		PlayerPrefsX.SetBoolArray("CompleteLevel" + gNum, tmpArr);
	}

	private bool[] LoadFromSave(int gNum) {
//		Debug.Log("LOADS_FROM_SAVE");
		return PlayerPrefsX.GetBoolArray("CompleteLevel" + gNum);
	}

	private void LoadforTest(int gNum, int pNum) {
		//test this
		bool[] tmpArr2 = PlayerPrefsX.GetBoolArray("CompleteLevel" + gNum);

		Debug.Log ("LENGHT " + tmpArr2.Length);
		for (int i = 0; i < tmpArr2.Length; i++) {
			Debug.Log(tmpArr2.GetValue(i)); 
		}
	}

	public static bool[] LoadS(int gNum) {
//		Debug.Log("LOADS");
		bool[] tmpArr = Camera.main.GetComponent<SaveLoadDataMenu>().LoadFromSave(gNum);
		//Debug.Log (tmpArr.Length);
		return tmpArr;
	}

	private static void ParsingData() {

	}
}
