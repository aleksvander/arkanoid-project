using UnityEngine;
using System.Collections;

public class FireAnimation : MonoBehaviour {

	public GameObject ammoShot;
	public GameObject Gun;

	private float gunX;
	private float y_PosKar = -3.6f;

	private Animator anim_gun;
	private float time_create_left = 0.3f;
	private float time_create_right = 0.3f;

	public static bool ifFire;
	public bool isLeftFire = false;
	public bool isRightFire = false;
	private static float timer_out = 10.0f;
	public static bool pressFire = false;

	public float speed_gun = 1.2f;

	void Awake () {
		anim_gun = GetComponent<Animator>();
		isLeftFire = false;
		isRightFire = false;
		//FireAnimation.pressFire = false;
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (pressFire) {
			//Debug.Log (timer_out);
			if (ifFire) {
				anim_gun.SetBool("isFire", true);
				if (timer_out > 0) {
					timer_out -= Time.deltaTime;	
				} else {
					FireAnimation.ifFire = false;
				}
			} else {
				anim_gun.SetBool("isFire", false);
				timer_out = 10f;
			}

			if (timer_out < 0) {
				ActiveDeButtons.StackActionForStatus(false); 
				//timer_out = 10f;
				ifFire = false;
				StopBonusFire();
				StopAnimator();
			}

			gunX = Gun.transform.position.x;
			//Debug.Log (gunX);
			time_create_left -= Time.deltaTime;
			time_create_right -= Time.deltaTime;

			//if (ifFire) {
				//anim_gun.Play();
			anim_gun.speed = speed_gun;

			if (ifFire) {
				if (isLeftFire && time_create_left < 0) {
					//Debug.Log (" isLeftFire " + isLeftFire);
					GameObject newObject = Instantiate(ammoShot) as GameObject;
					newObject.transform.position = new Vector3(gunX-0.4f, y_PosKar,0);
					newObject.gameObject.tag = "Shot";
					time_create_left = 0.1f;
				}

				if (isRightFire && time_create_right < 0) {
					GameObject newObject = Instantiate(ammoShot) as GameObject;
					newObject.transform.position = new Vector3(gunX+0.4f, y_PosKar,0);
					newObject.gameObject.tag = "Shot";
					time_create_right = 0.1f;
				}
			}
		}

		//}
	}

	public void StopBonusFire() {
		ifFire = false;
		timer_out = 0f;
		ifFire = false;
		isLeftFire = false;
		isRightFire = false;
	}

	public void StopAnimator() {
		ifFire = false;
		isLeftFire = false;
		isRightFire = false;
	}

	public static void ResetTimer() {
		timer_out = 10f;
	}
}
