using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReSelectTexture : MonoBehaviour {

	public List<Material> CollectTexture = new List<Material>();

	public void SelectTexture(int _numTexture) {
		this.gameObject.renderer.material = CollectTexture[_numTexture];
	}
}
