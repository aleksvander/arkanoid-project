using UnityEngine;
using System.Collections;

public class RotateBlock : MonoBehaviour {

	public bool isRotate;
	public bool isChasovoi;
	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isRotate) {
			if (isChasovoi) {
				transform.Rotate(0, 0, Time.deltaTime * speed);
			} else {
				transform.Rotate(0, 0, Time.deltaTime * -speed);
			}
		}
	}
}
