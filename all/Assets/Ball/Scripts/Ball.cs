using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ball : MonoBehaviour {
/*	//Скорость
	public const float MIN_SPEED = 2;
	public float startSpeed = 5;
	public const float MAX_SPEED = 15;

	//Тут выбрать каретку
	public GameObject karetka;
	private Vector3 PossitionKaretka;

	//Магнит
	public bool isReturnSpeed;
	private float Raznica_X;
	private float Raznica_Y;

	//Выравнивание скорости
	private RaycastHit rLazer;
	public bool _isIncreateSpeed;
	private float saveSpeed = 10f;
	private bool _isSaveSpeed = true;
	private float outTimerForNotCollisium = 0.1f;
	private float timerAfterCol = 0.05f;
	private float procentX;
	private float procentY;
	private float raznicaSpeed;
	private bool _isAfterCol = false;
	private float procentXs;
	private float procentYs;

	//Приведение колизии к положительному числу для оперирования
	private float colOtricX;
	private float colOtricY;

	//Расчет скорости
	public float incSpeedX = 0.1f;
	public float incSpeedY = 0.1f;
	public float curSpeed = 0;

	//Проверка на старт
	private bool _isStarted = false;
	private bool _isCollisiumEdit = false;

	//Импульс старта
	Vector3 MoveVec = new Vector3(1.5f,1.7f,0);

	//Текущее движение
	Vector3 thisMoveVec = new Vector3(0, 0, 0);

	//Объекты столкновения
	private string firstWall;
	private string secondWall;

	//Таймер
	private float timer = 0.11f;

	private Transform _t;

	private float tempX;
	private float tempY;

	void Awake() {
		_t = transform;
		
		//blocks = new List<GameObject>();
	}

	void Start() {
		rigidbody.AddRelativeForce( new Vector3 (startSpeed*40, startSpeed*60, 0) );
		_isCollisiumEdit = true;
/*		tempX = 0.001f;
		tempY = 0.001f;
		if (rigidbody.velocity.x < 0) {
			tempX = -tempX;
		} else { tempX = tempX; }
		if (rigidbody.velocity.y < 0) {
			tempY = -tempY;
		} else { tempY = tempY; }
		rigidbody.velocity += new Vector3(tempX, tempY, 0);
		Debug.Log ("SPEED LAST UP " + curSpeed);*/
//	}

	/*void Update() {
		if (Input.GetMouseButtonDown(0) && _isCollisiumEdit)
		{
			MagnitCollisium();
			//if (Input.GetMouseButtonUp(0)) _isCollisiumEdit = false;
		}

	}

	/*void FixedUpdate () {
		curSpeed = Vector3.Magnitude(rigidbody.velocity);

		ReturnSpeed ();

		if (curSpeed > MAX_SPEED) rigidbody.velocity /=curSpeed / MAX_SPEED;

		if (curSpeed < MIN_SPEED) rigidbody.velocity /= curSpeed / MIN_SPEED;

		if (_isAfterCol) {
			//timerAfterCol -= Time.deltaTime;
			//Debug.Log ("timerAfterCol: " + timerAfterCol);
			//if (timerAfterCol < 0) {
				CorrectSpeedAfterCol ();
				//Debug.Log ("Correct SPEED1: " + rigidbody.velocity);
				//if (raznicaSpeed > 0.2f) {	
				rigidbody.velocity += new Vector3(procentXs + 0.000000f, procentYs + 0.000000f, 0);
					Debug.Log ("Correct SPEED2: " + rigidbody.velocity);
				//}
				_isAfterCol = false;

				outTimerForNotCollisium = 3f;
				timerAfterCol = 0.05f;

				Debug.Log("UNLOCK SAVE SPEED");
				_isSaveSpeed = true;
			//}
		}
	}*/

	/*void ReturnSpeed () {
		if (isReturnSpeed) {
			//Debug.DrawRay(transform.position, rigidbody.velocity / 4, Color.red);
			outTimerForNotCollisium -= Time.deltaTime;

			//if (outTimerForNotCollisium < 0) {
				//Debug.Log ("outTimerForNotCollisium: " + outTimerForNotCollisium);
				//_isSaveSpeed = true;

			
				if (_isSaveSpeed) {
					if(Physics.Raycast(transform.position, rigidbody.velocity, out rLazer, 2f)){

						if (rigidbody.velocity.x < 0) {
							colOtricX = -rigidbody.velocity.x;
						} else { colOtricX = rigidbody.velocity.x; }
						if (rigidbody.velocity.y < 0) {
							colOtricY = -rigidbody.velocity.y;
						} else { colOtricY = rigidbody.velocity.y; }
						
						//saveSpeed = colOtricX + colOtricY;//Vector3.Magnitude(rigidbody.velocity);

						Debug.Log("SPEED IS SAVE " + saveSpeed);
						_isSaveSpeed = false;
						outTimerForNotCollisium = 3f;
					//Debug.Log ("This collisium: " + rigidbody.velocity + " Save speed: " + saveSpeed);
					}
				}
			//}
		}
	}*/
/*
	void OnCollisionEnter (Collision col) {
		//Debug.Log ("Hit: " + col.gameObject.name);
		rigidbody.isKinematic = true;



		//Повышение скорости по таймеру при ударе
		timer -= Time.deltaTime;
		if (timer < 0) {
			SpeedUpKinematic();
		}

		//Если сталкиваемся с бампером (проверка через RayCast, нужно сохранить скорость перед столкновением и вернуть ее после

		//Гравитация
		//rigidbody.velocity += new Vector3 (0, -0.01f, 0);
		//Debug.Log ("SPEED LAST UP " + curSpeed);

		/*if (col.gameObject.name == "left") {
			firstWall = col.gameObject.name;
			Debug.Log ("LEFT");
		}
		if (col.gameObject.name == "right") {
			secondWall = col.gameObject.name;
			Debug.Log ("RIGHT");
		}
		if (firstWall == "left" && secondWall == "right") {
			//Гравитация
			//rigidbody.velocity += new Vector3 (0, -0.05f, 0);
			Debug.Log ("GRAVITATION");
		} else {
			//Ускорение
			//rigidbody.velocity += rigidbody.velocity * incSpeed;
			Debug.Log ("SPEED");
		}
		if (col.gameObject.name !="left" && col.gameObject.name != "right") {
			firstWall = "";
			secondWall = "";
			Debug.Log ("RESET");
		}*/

		//Возвращаем возможность сканировать объекты на столкновения _isSaveSpeed метода ReturnSpeed
/*		if (_isSaveSpeed == false) {

			_isAfterCol = true;

		}
		rigidbody.isKinematic = false;
	}*/
/*
	void CorrectSpeedAfterCol () {
		if (rigidbody.velocity.x < 0) {
			colOtricX = -rigidbody.velocity.x + 0.000000f;

		} else { colOtricX = rigidbody.velocity.x; }
		if (rigidbody.velocity.y < 0) {
			colOtricY = -rigidbody.velocity.y + 0.000000f;

		} else { colOtricY = rigidbody.velocity.y; }

		//Debug.Log ("colOtricX: " + colOtricX + " colOtricY " + colOtricY);

		float colisSpeed = colOtricX + colOtricY;//Vector3.Magnitude(rigidbody.velocity);

		//Debug.Log ("colisSpeed: " + colisSpeed);

		procentX = (colOtricX * 100f) / colisSpeed;
		procentY = (colOtricY * 100f) / colisSpeed;

		Debug.Log ("procentX: " + procentX + " procentY: " + procentY);

		//float saveSpeedX = ReturnSpeedMagnitudeForVelocity();
		//colisSpeed = ReturnSpeedMagnitudeForVelocity(saveSpeed);

		Debug.Log ("saveSpeed: " + saveSpeed);

		if (saveSpeed > colisSpeed) {
			raznicaSpeed = saveSpeed - colisSpeed;
		} else if (saveSpeed < colisSpeed) {
			//raznicaSpeed = saveSpeed - colisSpeed;

			//raznicaSpeed = 0;
		} else if (saveSpeed == colisSpeed) {
			//Значит делать ничего не надо... но об этом позже
			raznicaSpeed = 0;
		}
		//Debug.Log ("raznicaSpeed: " + (raznicaSpeed + 0.000000f));

			
			procentXs = (procentX * raznicaSpeed) / 100f;
			procentYs = (procentY * raznicaSpeed) / 100f;
			Debug.Log("procentXs " + procentXs + " procentYs " + procentYs + " raznicaSpeed " + raznicaSpeed);
			
			
			//Не прокатывает т.к. нужно так же сделать конвертацию из минуса в минуса из плюса в плюс
			if (rigidbody.velocity.x < 0) {
				procentXs = -procentXs;
			} 
			if (rigidbody.velocity.y < 0) {
				procentYs = -procentYs;
			}
			/*_isAfterCol = true;
		} else { _isAfterCol = false; }*/

		//_isSaveSpeed = true;
/*	}

	float ReturnSpeedMagnitudeForVelocity(float _saveSpeed) {
		//Возводим в степень т.к. Magnitude имеет формулу sqrt(x*x+y+y*z+z)
		_saveSpeed = _saveSpeed * _saveSpeed;
		return _saveSpeed;
	}*/
/*
	void SpeedUpKinematic() {
		if (_isIncreateSpeed) {
			//Debug.Log ("SPEED DO UP " + curSpeed);
			//Debug.Log (rigidbody.velocity);

			if (rigidbody.velocity.x == 0) {
				incSpeedX = 0;
			} else if (rigidbody.velocity.x > 0) {
				incSpeedX = 0.1f;
			} else if (rigidbody.velocity.x < 0) {
				incSpeedX = -0.1f;
			}

			if (rigidbody.velocity.y == 0) {
				incSpeedY = 0;
			} else if (rigidbody.velocity.y > 0) {
				incSpeedY = 0.1f;
			} else if (rigidbody.velocity.y < 0) {
				incSpeedY = -0.1f;
			}

			rigidbody.velocity += new Vector3(incSpeedX, incSpeedY, 0);

			//Debug.Log ("SPEED LAST UP " + curSpeed);

			timer = 0.11f;
		}
	}*/
/*
	void MagnitCollisium () {
		//_t.position = new Vector3(Random.Range( -Player.xBoundry + 2, Player.xBoundry - 2), -7, Player.zPosition);
		//_t.LookAt(GameObject.Find( karetka.name ).transform.position);
		PossitionKaretka = GameObject.Find( karetka.name ).transform.position;
		//Debug.Log (PossitionKaretka.y / 10f);
		Raznica_X = transform.position.x + PossitionKaretka.x;
		Raznica_X = (Raznica_X * -1f) / 5.0f;
		Debug.Log("X: " + Raznica_X);
		Raznica_Y = transform.position.y + PossitionKaretka.y;
		Raznica_Y = (Raznica_Y * -1f) / 5.0f;
		Debug.Log("Y: " + Raznica_Y);
		//Нужно найти способ сделать медленное повторение каждое определенное время (1 секунда например)
		//Mathf.Repeat(5f, Debug.Log (45));
		//и тем самым будет меняться траектория
		//rigidbody.velocity += new Vector3(Mathf.Repeat((Raznica_X * -1f) \ 5), 0, 0);
		rigidbody.velocity += new Vector3(Raznica_X, Raznica_Y, 0);
		//rigidbody.AddForce(PossitionKaretka);
	}*/
}
