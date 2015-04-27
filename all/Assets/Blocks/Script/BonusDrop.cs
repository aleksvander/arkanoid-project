using UnityEngine;
using System.Collections;

public class BonusDrop : MonoBehaviour {

	//prefabs
	public GameObject bombPrefab;
	public GameObject lifePrefab;
	public GameObject multiple5Prefab;
	public GameObject multiple3Prefab;
	public GameObject gunPrefab;
	public GameObject smallBallPrefab;
	public GameObject ballNormalPrefab;
	public GameObject speedUPPrefab;
	public GameObject speedDownPrefab;
	public GameObject bigShieldPrefab;
	public GameObject smallSheildPrefab;
	public GameObject fireBallPrefab;

	//bool
	private bool is_bomb = false;
	private bool isLife;
	private bool isMultiple3;
	private bool isMultiple5;
	private bool isGun;
	private bool isSmallBall;
	private bool isBallNormal;
	private bool isSpeedUp;
	private bool isSpeedDown;
	private bool isShieldUp;
	private bool isSheildDown;
	private bool isFireBall;

	public bool isBonusBlock;
	public bool onScript;
	private Vector3 _point;



	public void Awake() {

		_point = transform.position;
	}

	public bool GetStatus() {
		return onScript;
	}

	public void CreateBonus() {
		if (is_bomb) CreateBomb();
		if (isMultiple3) CreateBonus(multiple3Prefab);
		if (isMultiple5) CreateBonus(multiple5Prefab);
		if (isGun) CreateBonus(gunPrefab);
		if (isLife) CreateBonus(lifePrefab);
		if (isGun) CreateBonus(gunPrefab);
		if (isSmallBall) CreateBonus(smallBallPrefab);
		if (isBallNormal) CreateBonus(ballNormalPrefab);
		if (isSpeedUp) CreateBonus(speedUPPrefab);
		if (isSpeedDown) CreateBonus(speedDownPrefab);
		if (isShieldUp) CreateBonus(bigShieldPrefab);
		if (isSheildDown) CreateBonus(smallSheildPrefab);
		if (isFireBall	) CreateBonus(fireBallPrefab);
		//offAllParameter();
		testNaBonus();
	}

	public void SetBonusEnabled(string _typeBonus) {
		//Debug.Log ("READING LOGIC THIS " + _typeBonus);
		if (_typeBonus == "bomb" || _typeBonus == "Bomb") {
			is_bomb = true;
		} else { is_bomb = false; }

		if (_typeBonus == "life" || _typeBonus == "Life") {
			isLife = true;
		} else { isLife = false; }

		if (_typeBonus == "Multipli5" || _typeBonus == "multipli5") {
			isMultiple5 = true;
		} else { isMultiple5 = false; };

		if (_typeBonus == "Multipli3" || _typeBonus == "multipli3") {
			isMultiple3 = true;
		} else { isMultiple3 = false; };

		if (_typeBonus == "Gun" || _typeBonus == "gun") {
			isGun = true;
		} else { isGun = false; };

		if (_typeBonus == "small" || _typeBonus == "Small" || _typeBonus == "smallBall") {
			isSmallBall = true;
		} else { isSmallBall = false; };

		if (_typeBonus == "norm" || _typeBonus == "Norm" || _typeBonus == "normalBall") {
			isBallNormal = true;
		} else { isBallNormal = false; };

		if (_typeBonus == "speedup" || _typeBonus == "Speedup" || _typeBonus == "speedUp" || _typeBonus == "SpeedUp") {
			isSpeedUp = true;
		} else { isSpeedUp = false; };

		if (_typeBonus == "speeddown" || _typeBonus == "Speeddown" || _typeBonus == "speedDown" || _typeBonus == "SpeedDown") {
			isSpeedDown = true;
		} else { isSpeedDown = false; };

		if (_typeBonus == "shieldup" || _typeBonus == "Shieldup" || _typeBonus == "shieldUp" || _typeBonus == "ShieldUp" || _typeBonus == "bigShield") {
			isShieldUp = true;
		} else { isShieldUp = false; };

		if (_typeBonus == "shielddown" || _typeBonus == "Shielddown" || _typeBonus == "shieldDown" || _typeBonus == "ShieldDown" || _typeBonus == "smallSheild") {
			isSheildDown = true;
		} else { isSheildDown = false; };

		if (_typeBonus == "fireball" ) {
			isFireBall = true;
		} else { isFireBall = false; };
		//if  все переменные выключены onScript = false
	}

	private void CreateBomb() {

		if (bombPrefab != null) {
			_point = transform.position;
			//Debug.Log (_point);
			GameObject newObject = Instantiate(bombPrefab) as GameObject;
			newObject.transform.position = _point;

			newObject.gameObject.tag = "Bonus";
		}
	}

	private void CreateBonus(GameObject _prefab) {
		Debug.Log (_prefab.name);
		_point = transform.position;
		//Debug.Log (_point);
		GameObject newObject = Instantiate(_prefab) as GameObject;
		newObject.transform.position = _point;
		newObject.gameObject.tag = "Bonus";
	}

	private void OffAllParameter() {
		is_bomb = false;
	}

	private void testNaBonus() {
		if (!is_bomb && !isLife && !isMultiple3 && !isMultiple5 && !isGun && !isSmallBall && 
		    !isBallNormal && !isSpeedUp && !isSpeedDown && !isShieldUp && !isSheildDown && !isFireBall) {
			if (isBonusBlock) {
				CreateRandomBonus();
			}
		}
	}

	private void CreateRandomBonus() {
		int tmp = Random.Range(1 , 12);
		//Debug.Log("tmp" + tmp);
		switch (tmp)
		{ 
		case 1:
			CreateBomb();
			break;
		case 2:
			CreateBonus(multiple3Prefab);
			break;
		case 3:
			CreateBonus(multiple5Prefab);
			break;
		case 4:
			CreateBonus(gunPrefab);
			break;
		case 5:
			CreateBonus(lifePrefab);
			break;
		case 6:
			CreateBonus(gunPrefab);
			break;
		case 7:
			CreateBonus(smallBallPrefab);
			break;
		case 8:
			CreateBonus(ballNormalPrefab);
			break;
		case 9:
			CreateBonus(speedUPPrefab);
			break;
		case 10:
			CreateBonus(speedDownPrefab);
			break;
		case 11:
			CreateBonus(bigShieldPrefab);
			break;
		case 12:
			CreateBonus(smallSheildPrefab);
			break;
		default:
			break;
		}
	}
}
