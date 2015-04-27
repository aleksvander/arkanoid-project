using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class CloneBall : MonoBehaviour {

	public GameObject ballPrefab;
	public bool thatAll;
	public List<float> minimalList = new List<float>();


	public void SetClone(int _numberClone) {
		GlobalSpeed.ignore_inc_speed = true;
		if (_numberClone > 0 && _numberClone < 10) {
			CreateClone(_numberClone);
		}
	}
	
	// Update is called once per frame
	private void CreateClone (int _numberClone) {
		Vector3 _t = transform.position;
		//float _currentProcentX = CalculateSpeedAsProcent(rigidbody.velocity.x, rigidbody.velocity.y);
		//float _currentProcentY = _currentProcentX - 100f;

		List<float> _list = new List<float>();
		_list = (GenerateSunLineForCreateBalls(_numberClone));
		float _speed = GetSpeed();
		GlobalSpeed.create_spark_logic = true;
		GlobalSpeed.timer_cheker = _numberClone;

		for (int i = 1; i < _numberClone; i++) {

			int schetchik = 0;

			object[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
			foreach(GameObject thisBall in allBalls) schetchik = schetchik + 1;
			//if ((float) _numberClone > GlobalSpeed.timer_cheker) GlobalSpeed.timer_cheker = (float) (schetchik / 2);
			if (schetchik < 50) {
				GameObject _ballPrefab = Instantiate(ballPrefab) as GameObject;
				//_ballPrefab.GetComponent<Collider>().enabled = false;
				//_ballPrefab.transform.position = new Vector3 (_t.x + (i/100), i + (i/100), 0);
				_ballPrefab.transform.position = _t;
				_ballPrefab.gameObject.tag = "Ball";
				_ballPrefab.GetComponent<CloneBall>().enabled = false;
				_ballPrefab.GetComponent<BallV2>().isResetBall = false;
				GlobalSpeed.create_spark_logic = true;
				_ballPrefab.name = "BallS";
				//_ballPrefab.GetComponent<BallV2>().enabled = false;
				//_ballPrefab.rigidbody.isKinematic = true;
				// _ballPrefab.gameObject.rigidbody.velocity = getRandomVectorS(i, _numberClone);
				//Debug.Log ("BALLS SAMI SEBA DOLSHNI ZAPUSKAT!!!");
				//_ballPrefab.GetComponent<BallV2>().PlayStart();

				//getRandomVectorS = DELETE?
				if (i <= _list.Count) {
					Vector3 _velocity = new Vector3(ConvertProcentToSpeed(_list[i - 1], _speed), ConvertProcentToSpeed((100f - _list[i - 1]), _speed), 0);
				
				//Debug.Log(_velocity);
					_ballPrefab.GetComponent<BallV2>().CustomStart(_velocity);
				}
				//for _list(i t.e.//index)
				//method iz procent v speed
				//end for
			}
		}
	}

	private float ConvertProcentToSpeed(float _f, float _speed) {
		Debug.Log ("CONVERT: " + " f: " + _f + " speed: " + _speed + " result " + (_f * _speed) / 100f);
		return ((_f * _speed) / 100f);
	}


	private List<float> GenerateSunLineForCreateBalls(int _numberClone) {
		List<float> _list = new List<float>();
		float procentX = GetProcent();
		//float procentY = 100f - procentX;

		if (_numberClone == 3) {
			//+33% +33% +33% if >100 then -100 and +33% +33% +33% and add List... all
			for (int i = 1; i <= 3; i++) {
				procentX += 33;
				if (procentX > 100f) procentX -= 100f;
				//Debug.Log (procentX);
				_list.Add(procentX);
			}
		}
		if (_numberClone == 5) {
			for (int i = 1; i <= 3; i++) {
				procentX += 20f;
				if (procentX > 100f) procentX -= 100f;
				_list.Add(procentX);
			}			
		}
		return _list;
	}

	/*private Vector3 getRandomVectorS(int _i, int _numberClone) {
		float _speed = GetSpeed();
		float procentX = GetProcent();
		float procentY = 100 - procentX;
		Vector3 _velocity = new Vector3(1,1,0);
		float corX;
		float corY;

		if (_numberClone == 3) {
		// 25% 75%
			if (_i == 1) {
				corX = CalculateSpeedAsProcent(_speed, 25f);
				if (corX > _speed) {
					corY = corX - _speed;
				} else {
					corY = _speed - corX;
				}
				_velocity = new Vector3(corX, corY, 0);
			}
		// 50% 50%
			if (_i == 2) {
				corX = CalculateSpeedAsProcent(_speed, 50f);
				if (corX > _speed) {
					corY = corX - _speed;
				} else {
					corY = _speed - corX;
				}
				_velocity = new Vector3(corX, corY, 0);				
			}
		// 75% 50%
			if (_i == 3) {
				corX = CalculateSpeedAsProcent(_speed, 75f);
				if (corX > _speed) {
					corY = corX - _speed;
				} else {
					corY = _speed - corX;
				}
				_velocity = new Vector3(corX, corY, 0);								
			}
		}
		if (_numberClone == 5) {

		// 15% 85%
			if (_i == 1) {
				corX = CalculateSpeedAsProcent(_speed, 18f);
				if (corX > _speed) {
					corY = corX - _speed;
				} else {
					corY = _speed - corX;
				}
				_velocity = new Vector3(corX, corY, 0);								
			}
		// 30% 70%
			if (_i == 2) {
				corX = CalculateSpeedAsProcent(_speed, 36f);
				if (corX > _speed) {
					corY = corX - _speed;
				} else {
					corY = _speed - corX;
				}
				_velocity = new Vector3(corX, corY, 0);								
			}
		// 45% 55%
			if (_i == 3) {
				corX = CalculateSpeedAsProcent(_speed, 54f);
				if (corX > _speed) {
					corY = corX - _speed;
				} else {
					corY = _speed - corX;
				}
				_velocity = new Vector3(corX, corY, 0);								
			}
		// 60% 40%
			if (_i == 4) {
				corX = CalculateSpeedAsProcent(_speed, 72f);
				if (corX > _speed) {
					corY = corX - _speed;
				} else {
					corY = _speed - corX;
				}
				_velocity = new Vector3(corX, corY, 0);								
			}
		// 75% 25%
			if (_i == 5) {
				corX = CalculateSpeedAsProcent(_speed, 90f);
				if (corX > _speed) {
					corY = corX - _speed;
				} else {
					corY = _speed - corX;
				}
				_velocity = new Vector3(corX, corY, 0);								
			}
		} 
		return _velocity;
	}*/

	private float CalculateSpeedAsProcent(float _speed, float _proc) {
		return (_proc * _speed) / 100f;
	}

	private float GetProcent() {
		Vector3 _velocity = getVelocityVectroS();
		float _speed = GetSpeed();
		float _procent = (_velocity.x * 100f) / _speed;
		return _procent;
	}

	private float GetSpeed() {
		return GlobalSpeed.GetSetGlobalSpeed;//rigidbody.velocity.x + rigidbody.velocity.y;
	}

	private Vector3 getVelocityVectroS() {
		return rigidbody.velocity;
	}

	//test
	/*
	void Update() {
		if (thatAll) { 
			CreateClone(count_clone);
			thatAll = false;
		}

	}*/
}
