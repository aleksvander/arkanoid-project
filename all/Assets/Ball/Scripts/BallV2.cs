using UnityEngine;
using System.Collections;

public class BallV2 : MonoBehaviour {
	//Магнит
	public GameObject karetka;
	private Vector3 possitionKaretka;
	private float raznica_X;
	private float raznica_Y;
	public bool _isCollisiumEdit;
	public float Y_START_POS = -3.3f;

	//Скорость
	public const float MIN_SPEED = 2;
	public float startSpeed = 5;
	public const float MAX_SPEED = 15;

	//ReturnSpeed
	private float colisSpeed;
	private float colOtricX;
	private float colOtricY;
	private float procentX;
	private float procentY;
	private float procentXs;
	private float procentYs;
	private float saveSpeed;
	private float raznicaSpeed;

	//Игнор мячей
	private float timerIgnorAll = 0.1f;
	//private bool customIsStart = false;
	private Vector3 _customVectorStart;
	public GameObject prefabs_balls;

	public bool _isIncreateSpeed;
	//private bool _isSaveSpeed = true;
	//private bool _isAfterCol = false;

    //private float outTimerForNotCollisium = 0.5f;
	//private float timerAfterCol = 0.05f;

	//Старт-Ресет
	public bool isResetBall = true;
	public GameObject kCar;
	public GameObject ammortizatorBox;

	//Управляющие переменные
	private bool isMagniteButton;
	private bool isGo;

	void Awake() {
		//rigidbody.isKinematic = true;
		karetka = GameObject.Find ("Collider_raketka");
		kCar = GameObject.Find("BodyCar");
		ammortizatorBox = GameObject.Find ("AmmortizatorBox");
	}

	void Start() {
		karetka = GameObject.Find ("Collider_raketka");
		kCar = GameObject.Find("BodyCar");
		CorrectSpeedGlobal();
		saveSpeed = GlobalSpeed.GetSetGlobalSpeed;
		//Physics.IgnoreCollision(prefabs_balls.gameObject.collider, collider, true);
	}

	public void PlayStart() {
		//Debug.Log ("PLAYSTART");
		//rigidbody.AddRelativeForce( new Vector3 (startSpeed*40, startSpeed*60, 0) );
		if (!rigidbody.isKinematic) rigidbody.isKinematic = false;
		saveSpeed = GlobalSpeed.GetSetGlobalSpeed;
		rigidbody.isKinematic = false;
		rigidbody.velocity = new Vector3(saveSpeed / 2, saveSpeed / 2, 0);
		//_isCollisiumEdit = true;
	}

	public void CustomStart(Vector3 _velocity) {
		if (!rigidbody.isKinematic) rigidbody.isKinematic = false;
		saveSpeed = GlobalSpeed.GetSetGlobalSpeed; //_velocity.x + _velocity.y;
		//Debug.Log (" VVV: " + _velocity.x + " " + _velocity.y);
		gameObject.GetComponent<BallV2>().enabled = true;
		if (transform.position.y > -5.6f) {
			rigidbody.velocity = new Vector3(_velocity.x, _velocity.y, 0);
			//Debug.Log ("1");
		} else {
			rigidbody.velocity = new Vector3(saveSpeed / 2, saveSpeed / 2, 0);
			//Debug.Log ("2");
		}

		CorrectSpeedAfterCol();
	}

	public void preesedButtonMagnite() {
		isMagniteButton = true;
	}

	public void pressUpButtonMagnite() {
		isMagniteButton = false;
	}

	public void pressedButtonGo() {
		isGo = true;
	}

	public void pressUpButtonGo() {
		isGo = false;
	}

	//void Update() {

	//}

	private void CorrectSpeedGlobal() {

	}

	void Update() {
		//		Physics.IgnoreCollision(prefabs_balls.collider, collider);
		//if (rigidbody.velocity.magnitude == 0f) PlayStart();
		//Debug.Log (rigidbody.velocity);
		//		Debug.Log ("saveSpeed: " + saveSpeed);
		if (isResetBall) {
			transform.position = new Vector3(kCar.transform.position.x + 0.1f, Y_START_POS, 0);
			//Time.deltaTime
			//transform.Rotate(Vector3.up * Time.deltaTime * 10);
		}
		//Debug.Log ("CURRENT VELOCITY: NAME OBJECT= " + name + " | " + rigidbody.velocity);
		//if (Input.GetMouseButton(0) && _isCollisiumEdit && !isResetBall)
		//		Debug.Log ("SOSTOIANIE: " + isMagniteButton + " " + _isCollisiumEdit + " " + isResetBall);
		if (isMagniteButton && _isCollisiumEdit && !isResetBall)
		{
			//			Debug.Log ("DELAY");
			if (!isResetBall) MagnitCollisium();
		}
		
		if (isGo && isResetBall) {
			isResetBall = false;
			PlayStart();
		}
		
		if (timerIgnorAll > 0) {
			timerIgnorAll -= Time.deltaTime;
			CorrectSpeedGlobal();
		} else if (!collider.enabled) {
			collider.enabled = true;
		}
		
		if (isResetBall && !ammortizatorBox.GetComponent<Collider>().enabled) {
			isResetBall = false;
			PlayStart();
		}
		//CorrectSpeedAfterCol();
	}

/*	public void setResetBallFalls() {
		isResetBall = false;
	}*/

	void OnCollisionEnter (Collision col) {
		CorrectSpeedAfterCol();
		IncreateSpeed();

		if (collider.enabled) {
			if (timerIgnorAll > 0) 
			{ 
				Physics.IgnoreCollision(col.collider, collider);
			}

			if (col.gameObject.tag == "Ball" || col.gameObject.tag == "SmallBall") {
				//Debug.Log (col.gameObject.tag);
				Physics.IgnoreCollision(col.gameObject.collider, collider, true);
			}
			if (col.gameObject.tag == "Shot") {
				//Debug.Log (col.gameObject.tag);
				Physics.IgnoreCollision(col.gameObject.collider, collider, true);
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		CorrectSpeedAfterCol();
		IncreateSpeed();

		if (timerIgnorAll > 0) 
			{ 
				if (gameObject.GetComponent<Collider>().enabled) Physics.IgnoreCollision(col.collider, collider);
			} else {

			if (gameObject.GetComponent<SphereCollider>().enabled) {
				if (col.gameObject.tag == "Ball" || col.gameObject.tag == "SmallBall") {
//					Debug.Log (col.gameObject.tag);
					Physics.IgnoreCollision(col.gameObject.collider, collider, true);
				}
				if (col.gameObject.tag == "Shot") {
//					Debug.Log (col.gameObject.tag);
					Physics.IgnoreCollision(col.gameObject.collider, collider, true);
				}
			}
		}
	}
	
	
	void CorrectSpeedAfterCol () {
		if (rigidbody.velocity.x < 0) {
			colOtricX = -rigidbody.velocity.x + 0.000000f;	
		} else { 
			colOtricX = rigidbody.velocity.x; 
		}
		if (rigidbody.velocity.y < 0) {
			colOtricY = -rigidbody.velocity.y + 0.000000f;
		} else { 
			colOtricY = rigidbody.velocity.y; 
		}

		colisSpeed = colOtricX + colOtricY;
		if (colisSpeed == 0) colisSpeed = GlobalSpeed.GetSetGlobalSpeed;

		procentX = (colOtricX * 100f) / colisSpeed;
		procentY = (colOtricY * 100f) / colisSpeed;

		if (saveSpeed > colisSpeed) {
			raznicaSpeed = saveSpeed - colisSpeed;
		} else if (saveSpeed < colisSpeed) {
			raznicaSpeed = saveSpeed - colisSpeed;
		} else if (saveSpeed == colisSpeed) {
			raznicaSpeed = 0;
		}

		procentXs = (procentX * raznicaSpeed) / 100f;
		procentYs = (procentY * raznicaSpeed) / 100f;

		if (rigidbody.velocity.x < 0) {
			procentXs = -procentXs;
		} 
		if (rigidbody.velocity.y < 0) {
			procentYs = -procentYs;
		}

		//if (procentXs > 0 && procentYs > 0) 
		if (!isResetBall) {
			rigidbody.velocity += new Vector3(procentXs + 0.000000f, procentYs + 0.000000f, 0);
		}
		//Debug.Log ("Correct SPEED2: " + rigidbody.velocity);
	}

	void ReversePlusOrMinus() {

	}

	void IncreateSpeed() {
		if (_isIncreateSpeed && saveSpeed <= MAX_SPEED) {
			/*outTimerForNotCollisium -= Time.deltaTime;
			//Debug.Log ("outTimerForNotCollisium: " + outTimerForNotCollisium);
			if (outTimerForNotCollisium < 0) {
				GlobalSpeed.GetSetGlobalSpeed += 0.2f;
				saveSpeed = GlobalSpeed.GetSetGlobalSpeed;
			//	Debug.Log ("SPEED INCREATE");
				outTimerForNotCollisium = 0.5f; //increate timer в зависимости от скорости, чем выше тем медленее
			}*/
			GlobalSpeed.IncreateSpeed();
			saveSpeed = GlobalSpeed.GetSetGlobalSpeed;
		}
	}


	//Магнит - ОК!
	void MagnitCollisium () {
		possitionKaretka = GameObject.Find( karetka.name ).transform.position;
		float _x = transform.position.x;
		float _y = transform.position.y;

		float _xK = possitionKaretka.x;
		float _yK = possitionKaretka.y;

		if (_x < 0) {
		//	_x = -_x;
		}

		if (_y < 0) {
		//	_y = -_y;
		}

		if (_xK < 0) {
		//	_xK = -_xK;
		}

		if (_yK < 0) {
		//	_yK = -_yK;
		}

		raznica_X = _x - _xK;
		//Debug.Log (raznica_X);
		if (transform.position.x > possitionKaretka.x) {
			raznica_X = -raznica_X;
		} else {
			raznica_X = -raznica_X;
		}

		raznica_Y = _y - _yK;
		if (transform.position.y > possitionKaretka.y) {
			raznica_Y = -raznica_Y;
		} else if (raznica_Y < 0) {
			raznica_Y = -raznica_Y;
		}


		//Debug.Log(transform.position);
		//Debug.Log (possitionKaretka);
		//Debug.Log (raznica_X);

		rigidbody.velocity += new Vector3(raznica_X, raznica_Y, 0).normalized;
		CorrectSpeedAfterCol();
	}
}
