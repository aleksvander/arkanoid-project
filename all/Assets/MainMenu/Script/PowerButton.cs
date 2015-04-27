using UnityEngine;
using System.Collections;

public class PowerButton : MonoBehaviour {

	public Sprite[] sprites = new Sprite[10];

	//private string _nameSprite = "power_atlas_";
	private SpriteRenderer myRender;

	// Use this for initialization
	void Start () {
		myRender = this.transform.parent.gameObject.GetComponent<SpriteRenderer> ();
		ReSelectSkin (ScenesInformation.PowerInt);
		//Debug.Log ("START" + ScenesInformation.PowerInt);
		//reset
		//PlayerPrefs.DeleteKey ("Power");
	}
	
	void OnMouseUp() {
		int _num = ScenesInformation.PowerInt;
		bool _check = false;
		_num++;
		_check = ReSelectSkin(_num);
		if (_check) {
			ScenesInformation.PowerInt = _num;
			SavePower();
		} else {
			_num--;
		}
	}

	private bool ReSelectSkin(int _num) {
		if (sprites.Length > _num) {
			myRender.sprite = sprites [_num];
		} else {
			return false;
		}
		return true;
	}

	private void SavePower() {
		//Debug.Log ("SAVE" + ScenesInformation.PowerInt);
		PlayerPrefs.SetInt ("Power", ScenesInformation.PowerInt);
		PlayerPrefs.Save ();
	}
}
