using UnityEngine;
using System.Collections;

public class UpdateLevelNum : MonoBehaviour {

	private int _curNum;

	// Use this for initialization
	void Start () {
		_curNum = ScenesInformation.CurrentLevel;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (ScenesInformation.CurrentLevel);
		if (_curNum != ScenesInformation.CurrentLevel) {
			this.gameObject.GetComponent<GUIText>().text = "Level " + ScenesInformation.CurrentLevel;
		}
	}
}
