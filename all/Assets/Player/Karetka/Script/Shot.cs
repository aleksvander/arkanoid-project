using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	public float speed_shot = 15f;
	private float destroyTim = 0.0005f;

	void Start() {
		rigidbody.velocity = new Vector3(0, speed_shot, 0);
	}

	void Update () {
		if (destroyTim >= 0) {
			destroyTim -= Time.deltaTime;
		}
		if (this.gameObject.transform.position.x > 20 || this.gameObject.transform.position.y > 20 || this.gameObject.transform.position.x < -20 || this.gameObject.transform.position.y < -20) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter (Collider col) {
		//Debug.Log (col.tag);
		if (col.tag != "Bonus") {
			if (col.tag != "Ball" || col.tag != "SmallBall") {
				if (col.tag != "Spark") {
					if (destroyTim <= 0) {
						/*	if (gameObject.name == gameObject.name) {
							Debug.Log (gameObject.name);
							Destroy(gameObject);
						} else {
							Debug.Log (col.name);
							Destroy(gameObject);
						}*/
						this.gameObject.GetComponent<MeshRenderer>().enabled = false;
						this.gameObject.GetComponent<Collider>().enabled = false;
							//Destroy(gameObject);

					}
				}
			}
		}
	}
}
