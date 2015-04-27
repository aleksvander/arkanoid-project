using UnityEngine;
using System.Collections;

public class UpdateLevelNumberG : MonoBehaviour {

	private static int level_number_g = 1;
	private static int level_number_p = 1;
	private static string nameObj;
	private static int max_level;

	// Use this for initialization
	//void Start () {
		//level_number_g = 1;
	//}

	void Start() {

	}

	public static int GetLevel_number_g {
		get {
			return level_number_g;
		}
	}

	public static int GetLevel_number_p {
		get {
			return level_number_p;
		}
	}

	public static void incLevelNumbG() {
		if (nameObj == "Text_Level_Number") { 
			if (level_number_g < max_level) {
				level_number_g += 1; 
			} 
		}
		if (nameObj == "LevelList") {if (level_number_p < 9) level_number_p += 1; }
	}

	public static void decLevelNumbG() {
		if (nameObj == "Text_Level_Number") { if (level_number_g > 1) level_number_g -= 1; }
		if (nameObj == "LevelList") { if (level_number_p > 1) level_number_p -= 1; }
	}
	
	// Update is called once per frame
	void Update () {
		if (max_level == 0) {
			//max_level = SaveLoadDataMenu.numScenGs;
			Debug.Log ("EDIT TO ... max_level" + max_level);
		}
		nameObj = gameObject.name;
		if (gameObject.name == "Text_Level_Number") {
			if(guiText.text != "Level " + level_number_g) { guiText.text = "Level " + level_number_g; }
		}
		if (gameObject.name == "LevelList") {
			if(guiText.text != "lvl " + level_number_p) { guiText.text = "lvl " + level_number_p; }
		}
	}
}
