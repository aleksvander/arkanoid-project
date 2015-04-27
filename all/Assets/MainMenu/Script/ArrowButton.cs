using UnityEngine;
using System.Collections;

public class ArrowButton : MonoBehaviour {
	
	private int limitGLevel;

	// Use this for initialization
	void Start () {
		limitGLevel = ScenesInformation.CountScenesG;
		ScenesInformation.CurrentLevel = 1;
	}
	
	void OnMouseUp() {
		if (this.gameObject.name == "LeftArrow") {
			if (ScenesInformation.CurrentLevel > 1) {
				DecreateNum();
			}
		}
		if (this.gameObject.name == "RightArrow") {
			if (ScenesInformation.CurrentLevel < limitGLevel) {
				IncreateNum();
			}
		}
	}

	private void DecreateNum() {
		ScenesInformation.CurrentLevel--;
	}

	private void IncreateNum() {
		ScenesInformation.CurrentLevel++;
	}
}
