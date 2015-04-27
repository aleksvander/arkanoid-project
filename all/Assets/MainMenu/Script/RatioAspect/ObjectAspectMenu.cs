using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectAspectMenu : MonoBehaviour {

	public List<GameObject> PlaneMenu = new List<GameObject>();

	private GameObject ForeachSelectCollection(int numObject) {
		int i = 0;
		foreach (GameObject enemy in PlaneMenu) {
			if (i == numObject) {
				return enemy;
			}
			i++;
		}
		return null;
	}

	void Start () {
		for (int i = 0; i < PlaneMenu.Count; i++) EditObject (ForeachSelectCollection(i), CalculateScale());
	}


	private float CalculateScale () {
		//http://habrahabr.ru/post/169141/
		//Debug.Log (" Calculate " + camera.aspect);


		//10:16
		if (camera.aspect > 0.62 && camera.aspect < 0.63) {
			return -0.04409f;
		//2:3
		} else if (camera.aspect == 0.6666667) {
			//XScale_P = 0.0f;
		} else if (camera.aspect > 0.56 && camera.aspect < 0.57) {
		//9:16
			return -0.1069f;
		} else if (camera.aspect > 0.59 && camera.aspect < 0.61) {
			return -0.067f;
		}
		return 0;
	}

	private void EditObject (GameObject _nameObject, float XScaleS) {
		//Debug.Log (" Edit " + _nameObject + " XScaleS " + XScaleS);
		if (XScaleS != 0) _nameObject.transform.localScale += new  Vector3(XScaleS, 0, 0);
	}
}
