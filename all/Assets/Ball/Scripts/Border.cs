using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour {

	public const float MAXIMAL = 10.0f;

	// Update is called once per frame
	void Update () {
		if (transform.position.x > MAXIMAL || transform.position.y > MAXIMAL) {
			Destroy(gameObject);
		}
		if (transform.position.y < -MAXIMAL || transform.position.x < -MAXIMAL) {
			Destroy(gameObject);
		}
		if (transform.position.y < 0 && transform.position.y < -MAXIMAL) {
			//Destroy(gameObject);
		}
	}
}
