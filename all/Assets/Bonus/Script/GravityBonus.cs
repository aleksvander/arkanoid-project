using UnityEngine;
using System.Collections;

public class GravityBonus : MonoBehaviour {

	//public float speed_gravity;
	private float timerDestroy = 7;
	private float playerTimerDestroy = 0.2f;
	private bool playerContact = false;
	private const float GRAVITY_CONST = 4.75f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - speed_gravity, transform.localPosition.z);
		//rigidbody.AddForce(0, speed_gravity, 0);
		timerDestroy = timerDestroy - Time.deltaTime;
		if (timerDestroy < 0) TestDestroy();
		if (playerContact) playerTimerDestroy -= Time.deltaTime;
		if (playerTimerDestroy < 0) DestroyObject();
		//if (transform.position.y < -4 && !collider.enabled) collider.enabled = true;

		transform.Translate(Vector3.down * Time.deltaTime * GRAVITY_CONST, Space.World);
	}

	void OnTriggerEnter(Collider col) {

		if (col.tag == "Player") {
			//Debug.Log ("1: " + col);
			playerContact = true;
			//destroyObject();
			//if (name != "Bomb") DestroyObject();
		}
		
		if (col.tag == "Ball" || col.tag == "SmallBall" && (name == "Bomb" || name == "Bomb(Clone)")) {
			if (name == "Bomb" || name == "Bomb(Clone)") {
				DestroyObject();
			}
		}
	}


	private void TestDestroy() {
		if (transform.localPosition.y < 10) DestroyObject();
	}

	private void DestroyObject() {
		Destroy(gameObject);
	}
}
