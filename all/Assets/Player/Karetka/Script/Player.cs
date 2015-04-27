/// <summary>
/// Player.cs
/// Date 27.06.2013
/// Author AleksVander
/// 
/// Get the Player input
/// move paddle accordingly along the x axis 
/// 
/// make sure the paddle is always at 0 on the z axis 
/// make sure the paddle is always at the same position on the y axis
/// 
/// create side boundries the player can not move by 
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public const float Z_POSITION = 0;
	public const float Y_POSITION = -4.19f;
	public static float xBoundry = 10f;
	public float speed = 5;
	private static float speedS = 5f;

	public PlayerAnimationScript playerAnimationScript;
	private static bool isRightFire = false;
	private static bool isLeftFire = false;

	//Controls
	public bool isKeyboard = true;
	public static bool isScreenTouch = true;
	public static bool isAccelerometer = false;

	private Transform _t;
	
	[SerializeField]
	private static float horizontallimit = 2.5f; 
	private static float dragspeed = 0.2f;
	private static float horizontal_plus_limit = 0.75f;
	public static bool blockAllControls = false;
	
	private static Transform cachedTransform;
	
	private static Vector3 startingPos;
	private static float _posKar_x;

	//CarDestroy
	private bool destroyOn = false;
	private bool resurectionOn = false;

	public GameObject bodyCar;

	public GameObject bumper;
	public GameObject car;
	public GameObject clapan1_01;
	public GameObject clapan1_02;
	public GameObject clapan1_03;
	public GameObject crepleniePerpendik1;
	public GameObject crepleniePriamoe1;
	public GameObject gun1_down;
	public GameObject gun1_UP;
	public GameObject clapan2_01;
	public GameObject clapan2_02;
	public GameObject clapan2_03;
	public GameObject crepleniePerpendik2;
	public GameObject crepleniePriamoe2;
	public GameObject gun2_down;
	public GameObject gun2_up;
	public GameObject soploLeft;
	public GameObject soploRight;
	private List<GameObject> allMeshRender = new List<GameObject>();


	public GameObject collyderCar1;
	public GameObject collyderCar2;

	public GameObject gun;
	private float timer_resurection = 3;
	private float timer_blind = 3;
	private float timer_blind_action = 0.5f;



	void Awake () {
		_t = transform;
	}

	private void DefaultPosition() {
		_t.position = new Vector3(0, Y_POSITION, Z_POSITION);
	}

	private void LoadSelectControl() {
		if (ScenesInformation.TypeControl) {
			isScreenTouch = false;
			isAccelerometer = true;
		} else {
			isScreenTouch = true;
			isAccelerometer = false;
		}
//		Debug.Log("isScreenTouch" + isScreenTouch);
//		Debug.Log ("PumpControl.typeControlGlobal" + PumpControl.typeControlGlobal);
	}

	// Use this for initialization
	void Start () {
		LoadSelectControl();

		//_t.position = new Vector3(0, yPosition, zPosition);
		allMeshRender.Add(bumper);
		allMeshRender.Add(car);
		allMeshRender.Add(clapan1_01);
		allMeshRender.Add(clapan1_02);
		allMeshRender.Add(clapan1_03);
		allMeshRender.Add(crepleniePerpendik1);
		allMeshRender.Add(crepleniePriamoe1);
		allMeshRender.Add(gun1_down);
		allMeshRender.Add(gun1_UP);
		allMeshRender.Add(clapan2_01);
		allMeshRender.Add(clapan2_02);
		allMeshRender.Add(clapan2_03);
		allMeshRender.Add(crepleniePerpendik2);
		allMeshRender.Add(crepleniePriamoe2);
		allMeshRender.Add(gun2_down);
		allMeshRender.Add(gun2_up);
		allMeshRender.Add(soploLeft);
		allMeshRender.Add(soploRight);

		//Make reference to transform
		cachedTransform = transform;

		//Save start position
		startingPos = cachedTransform.position;
	}


	// Update is called once per frame
	void Update () {
		//updateStaticVaribles
		Player._posKar_x = transform.position.x;

		//flame
		FlameUpdateControl();

		//controllers
		if (!blockAllControls) {
			if (isKeyboard) PlayerControlKeyboard();
		}

		//bodyCarAction
		if (destroyOn) DestroyAction();

		//
		if (resurectionOn) {
			ResurectionOnUpdateAction();
		}

		//life correcotr
		if (CorrectLife.getLife() < 0) Debug.Log ("GAME OVER");
	}

	//AllControllers Method

	private static void SlowSpeed() {
		dragspeed = 0.04f;
	}

	private static void MidSpeed() {
		dragspeed = 0.05f;
	}

	private static void HightSpeed() {

	}

	private static void UpSpeed() {
		dragspeed += 0.01f;
	}

	private static void MoveAction(float tmpF, Vector3 deltaPosition) {
		//Debug.Log (deltaPosition.x);

		float tmpX = 1f;
		tmpX = horizontallimit / 4f;
		if (deltaPosition.x < 0) tmpX *= -1f;
	
        //float maxX = 0f;
        //if (_posKar_x > 0) {

        //} else {
        //    maxX = -0.05f;
        //}

		cachedTransform.position = new Vector3(Mathf.Clamp((tmpX * dragspeed * tmpF) + (cachedTransform.position.x + 0f), startingPos.x - horizontallimit - horizontal_plus_limit, startingPos.x + horizontallimit + horizontal_plus_limit), Y_POSITION, Z_POSITION);
		//Debug.Log (Mathf.Clamp(deltaPosition.x * dragspeed * tmpF));
	}

	public static void DragObject(Vector3 deltaPosition) {
		blockAllControls = true;

		int limit_x = 3;
		if (deltaPosition.x < 0) limit_x *= -1;

		if (Mathf.Round(_posKar_x * 10f) != Mathf.Round(deltaPosition.x * 10f) + limit_x) {

			//	Debug.Log ("deltaPosition " + Mathf.Round(deltaPosition.x * 10f) + " posKar " + Mathf.Round(_posKar_x * 10f));

			/*if (_posKar_x > 1f || _posKar_x < -1f) {
				if (_posKar_x > 0 && deltaPosition.x > _posKar_x) { SlowSpeed(); }
				if (_posKar_x > 0 && deltaPosition.x < _posKar_x) { MidSpeed(); }

				if (_posKar_x < 0 && deltaPosition.x < _posKar_x) { MidSpeed(); }
				if (_posKar_x < 0 && deltaPosition.x > _posKar_x) { HightSpeed(); }
			} else {
				if (_posKar_x > 0 && deltaPosition.x > _posKar_x) { MidSpeed(); }
				if (_posKar_x > 0 && deltaPosition.x < _posKar_x) { HightSpeed(); }
				
				if (_posKar_x < 0 && deltaPosition.x < _posKar_x) { HightSpeed(); }
				if (_posKar_x < 0 && deltaPosition.x > _posKar_x) { MidSpeed(); }
			}*/
			float tmpF = 1f;
			if (_posKar_x < 0f && deltaPosition.x < 0f && Mathf.Round(_posKar_x * 10f) < Mathf.Round(deltaPosition.x * 10f) + limit_x) tmpF = -1f;
			if (_posKar_x > 0f && deltaPosition.x > 0f && Mathf.Round(_posKar_x * 10f) > Mathf.Round(deltaPosition.x * 10f) + limit_x) tmpF = -1f;
			MoveAction(tmpF, deltaPosition);
			

			if (deltaPosition.x < _posKar_x) {
				RightFire();

			} else if (deltaPosition.x > _posKar_x) {
				LeftFire();

			} else {
				TurnOffAllFire();
			}
		} else {
			TurnOffAllFire();
		}
	}
	
	public static void TurnOffAllFire() {
		isLeftFire = false;
		isRightFire = false;
		blockAllControls = false;
	}
	
	public static void LeftFire() {
		isLeftFire = true;
	}
	
	public static void RightFire() {
		isRightFire = true;
	}

	private void FlameUpdateControl() {
		if (isRightFire) {
			playerAnimationScript.LeftAnimation();
		}
		if (isLeftFire) {
			playerAnimationScript.RightAnimation();
		}
		if (!isLeftFire && !isRightFire) {
			IdleAction();
		}
	}
	
	private void IdleAction() {
		playerAnimationScript.IdleAnimation();
	}

	private void PlayerControlKeyboard() {
		if (Input.GetAxis("Horizontal") != 0) {
			//Debug.Log(Input.GetAxis("Horizontal"));	
			//move paddle according to input along the x-axis
			float tmpXmax = _t.position.x;
			bool minus = false;
			if (tmpXmax < 0) { 
				tmpXmax *= -1f;
				minus = true;
			}

			//Debug.Log (Mathf.Round(tmpXmax * 100f) >= Mathf.Round((horizontallimit  + horizontal_plus_limit) * 100f));

			if (Mathf.Round(tmpXmax * 100f) >= Mathf.Round((horizontallimit  + horizontal_plus_limit) * 100f)) {
				if (minus) {
					_t.position = new Vector3 (-tmpXmax + 0.01f, Y_POSITION, Z_POSITION);
				} else {
					_t.position = new Vector3 (tmpXmax - 0.01f, Y_POSITION, Z_POSITION);
				}
			}

			if (Mathf.Round(tmpXmax * 100f) < Mathf.Round((horizontallimit  + horizontal_plus_limit) * 100f)){
				_t.position = new Vector3(_t.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, Y_POSITION, Z_POSITION);
			}

			//Debug.Log(Mathf.Round(tmpXmax * 100f) + " + " +  Mathf.Round((horizontallimit  + horizontal_plus_limit) * 100f));

			if (tmpXmax == horizontallimit + horizontal_plus_limit && Input.GetAxis("Horizontal") > 0) {
				_t.position = new Vector3(_t.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, Y_POSITION, Z_POSITION);
			} else if (tmpXmax == horizontallimit + horizontal_plus_limit && Input.GetAxis("Horizontal") < 0) {
				_t.position = new Vector3(_t.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, Y_POSITION, Z_POSITION); 
			}

			//check boundries - заменить на raycast или что то другое (проверка на границы)
			if (Input.GetAxis("Horizontal") < 0) {
				RightFire();
			} else if (Input.GetAxis("Horizontal") > 0) {
				LeftFire ();
			}
		} else {
			TurnOffAllFire();
		}
	}

	public static void AccelerometerAction(Vector3 tmpVec) {
		float tmpXmax = _posKar_x;
		bool minus = false;

		if (tmpXmax < 0) { 
			tmpXmax *= -1f;
			minus = true;
		}
		
		//Debug.Log (Mathf.Round(tmpXmax * 100f) >= Mathf.Round((horizontallimit  + horizontal_plus_limit) * 100f));
		
		if (Mathf.Round(tmpXmax * 100f) >= Mathf.Round((horizontallimit  + horizontal_plus_limit) * 100f)) {
			if (minus) {
				cachedTransform.position = new Vector3 (-tmpXmax + 0.01f, Y_POSITION, Z_POSITION);
			} else {
				cachedTransform.position = new Vector3 (tmpXmax - 0.01f, Y_POSITION, Z_POSITION);
			}
		}
		
		if (Mathf.Round(tmpXmax * 100f) < Mathf.Round((horizontallimit  + horizontal_plus_limit) * 100f)){
			cachedTransform.position = new Vector3(cachedTransform.position.x 
			                                       + tmpVec.x 
			                                       * speedS * Time.deltaTime, Y_POSITION, Z_POSITION);
			

			if (tmpVec.x < 0.15f) {
				RightFire();
			} else if (tmpVec.x > -0.15f) {
				LeftFire ();
			} else {
			TurnOffAllFire();
			}
		}
	}


	//AllActionBonusBombMethod

	private void ResurectionOnUpdateAction() {
		timer_resurection -= Time.deltaTime;
		if (timer_resurection <= 0) {
			RessurectionBlind();
		}
	}

	/*public void PlayerControlScreen(Vector3 _vector) {


		float _x = _vector.x;

		_x = _x / speed;

		if (_x != 0) {
			_x = _x * (-1);
		}

		if (_x > transform.position.x) {
			isLeftFire = true;
			isRightFire = false;
		} else if (_x < transform.position.x) {
			isLeftFire = false;
			isRightFire = true;
		} else {
			IdleAnimationOn();
		}

		//Debug.Log ((_x / 2.6f) - transform.position.x * speed * Time.deltaTime);
		Vector3 dir = new Vector3 (_x - transform.position.x * speed * Time.deltaTime, 0, 0);
		transform.Translate(dir);

		//_t.position = new Vector3(_t.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, Y_POSITION, Z_POSITION);
	}*/

	public void DestroyPlayer() {
		if (!destroyOn && !resurectionOn) {
			destroyOn = true;
			offAllControls();
			offAllBonus();
			//go BALL!
			bodyCar.GetComponent<PlayerAnimationScript>().AllStopAnimation();
			DefaultPosition();
		}
	}

	private void DestroyAction() {
		//Debug.Log ("DESTROY ACTION");
		offCaretka();
		//CorrectLife.decLife();
		destroyOn = false;
		resurectionOn = true;
		timer_blind = 3;
		AnimatorOFF();
		//playerAnimationScript.enabled = false;
	}

	private void offCaretka() {
		//if (bodyCar.activeSelf) bodyCar.SetActive(false);
		allMeshRender.ForEach(SwitcherMeshRenderOff);

		/*SwitcherMeshRender(car, false);
		SwitcherMeshRender(clapan1_01, false);
		SwitcherMeshRender(clapan1_02, false);
		SwitcherMeshRender(clapan1_03, false);
		SwitcherMeshRender(crepleniePerpendik1, false);
		SwitcherMeshRender(crepleniePriamoe1, false);
		SwitcherMeshRender(gun1_down, false);
		SwitcherMeshRender(gun1_UP, false);
		SwitcherMeshRender(clapan2_01, false);
		SwitcherMeshRender(clapan2_02, false);
		SwitcherMeshRender(clapan2_03, false);
		SwitcherMeshRender(crepleniePerpendik2, false);
		SwitcherMeshRender(crepleniePriamoe2, false);
		SwitcherMeshRender(gun2_down, false);
		SwitcherMeshRender(gun2_up, false);
		SwitcherMeshRender(soploLeft, false);
		SwitcherMeshRender(soploRight, false);*/

		if (collyderCar1.GetComponent<Collider>().enabled) collyderCar1.GetComponent<Collider>().enabled = false;
		if (collyderCar2.GetComponent<Collider>().enabled) collyderCar2.GetComponent<Collider>().enabled = false;
	}

	private void SwitcherMeshRenderOff(GameObject _name) {
		if (_name.GetComponent<MeshRenderer>().enabled) _name.GetComponent<MeshRenderer>().enabled = false;
	}

	private void SwitcherMeshRenderOn(GameObject _name) {
		if (!_name.GetComponent<MeshRenderer>().enabled) _name.GetComponent<MeshRenderer>().enabled = true;
	}

	private bool ResultSwtchMeshRender(GameObject _name) {
		return _name.GetComponent<MeshRenderer>().enabled;
	}

	private void onCaretka() {
		//if (!bodyCar.activeSelf) bodyCar.SetActive(true);
		/*SwitcherMeshRender(bumper, true);
		SwitcherMeshRender(car, true);
		SwitcherMeshRender(clapan1_01, true);
		SwitcherMeshRender(clapan1_02, true);
		SwitcherMeshRender(clapan1_03, true);
		SwitcherMeshRender(crepleniePerpendik1, true);
		SwitcherMeshRender(crepleniePriamoe1, true);
		SwitcherMeshRender(gun1_down, true);
		SwitcherMeshRender(gun1_UP, true);
		SwitcherMeshRender(clapan2_01, true);
		SwitcherMeshRender(clapan2_02, true);
		SwitcherMeshRender(clapan2_03, true);
		SwitcherMeshRender(crepleniePerpendik2, true);
		SwitcherMeshRender(crepleniePriamoe2, true);
		SwitcherMeshRender(gun2_down, true);
		SwitcherMeshRender(gun2_up, true);
		SwitcherMeshRender(soploLeft, true);
		SwitcherMeshRender(soploRight, true);*/
		allMeshRender.ForEach(SwitcherMeshRenderOn);

		if (!collyderCar1.GetComponent<Collider>().enabled) collyderCar1.GetComponent<Collider>().enabled = true;
		if (!collyderCar2.GetComponent<Collider>().enabled) collyderCar2.GetComponent<Collider>().enabled = true;
	}

	private void blindCaretka() {
		if (ResultSwtchMeshRender(bumper)) {
			allMeshRender.ForEach(SwitcherMeshRenderOff);
		} else if (!ResultSwtchMeshRender(bumper)) {
			allMeshRender.ForEach(SwitcherMeshRenderOn);
		}
	}

	private void RessurectionBlind() {
		timer_blind -= Time.deltaTime;
		collyderCar1.gameObject.GetComponent<ActionBonus>().AllBonusOffKaretka();
		if (timer_blind > 0) {
			timer_blind_action -= Time.deltaTime;
			if (timer_blind_action <= 0) {
				blindCaretka();
				timer_blind_action = 0.5f;
			}
		} else {
			timer_blind = 3f;
			timer_resurection = 3f;
			resurectionOn = false;
			returnControl();
			onCaretka();
			AnimatorON();
			//playerAnimationScript.enabled = true;
			bodyCar.GetComponent<PlayerAnimationScript>().AllResumeAnimation();
		}
	}

	private void offAllControls() {
		if (isKeyboard) isKeyboard = false;
	}

	private void returnControl() {
		//logic....

		isKeyboard = true; //заглушка
	}

	private void offAllBonus() {

	}

	private void AnimatorOFF() {
		gun.GetComponent<Animator>().enabled = false;
		gun.GetComponent<FireAnimation>().StopAnimator();
	}

	private void AnimatorON() {
		gun.GetComponent<Animator>().enabled = true;
	}
}
