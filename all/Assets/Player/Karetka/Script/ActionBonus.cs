using UnityEngine;
using System.Collections;

public class ActionBonus : MonoBehaviour {

	public GameObject player;
	private float MIN_SCALE = 0.21f;
	private float MAX_SCALE = 0.35f;

	private float MIN_SHIELD = 0.5f;
	private float MAX_SHIELD = 1.8f;

	private float tmp;

	public GameObject bamper;
	public GameObject shield_bamper;

	//private string a = " ";

	private delegate void SelectMethod(GameObject _go);

	void OnTriggerEnter(Collider col) {
		Debug.Log(col.gameObject.name);
		if (col.gameObject.tag == "Bonus") {
			CalculateScore.correctPrice(Price.BONUS);				
			BonusSelecter(col.name);
		}
	}

	void BonusSelecter(string _name) {
		//Life / unLife
		//Debug.Log ("Bonus selecter Active");
		//Debug.Log (_name);

		if (_name == "Bomb(Clone)") BonusBombAction();
		if (_name == "Life(Clone)" || _name == "Life") AddLife();

		//Size ball
		if (_name == "Ball_small(Clone)" || _name == "Small_size(Clone)" || _name == "Ball_small") SmallBall();
		if (_name == "Ball_normal(Clone)" || _name == "Normal_size(Clone)") ResizeNornal();

		//Speed
		if (_name == "SpeedUP(Clone)" || _name == "SpeedUP") { GlobalSpeed.IncGlobalSpeed(); };
		if (_name == "SpeedDown(Clone)" || _name == "SpeedDown") { GlobalSpeed.DecGlobalSpeed(); };

		//Gun
		if (_name == "Gun" || _name == "Gun(Clone)") { FireAnimation.ifFire = true; FireAnimation.ResetTimer();  ActiveDeButtons.StackActionForStatus(true); };

		//Shield
		if (_name == "Big_shield" || _name == "Big_shield(Clone)") { incShield(); };
		if (_name == "Small_shield" || _name == "Small_shield(Clone)") { decreateShield(); };

		//Multiple
		if (_name == "Multipli3" || _name == "Multipli3(Clone)") { Multiple3(); };
		if (_name == "Multipli5" || _name == "Multipli5(Clone)") { Multiple5(); };

		//FireBall
		if (_name == "Fire_ball" || _name == "Fire_ball(Clone)") { Debug.Log ("ЗАГЛУШКА"); };
	}

	private void incShield() {
		resizeShield(false);
	}

	private void decreateShield() {
		resizeShield(true);
	}

	private void reurnShield() {
		bamper.gameObject.transform.localScale = new Vector3 (bamper.gameObject.transform.localScale.x, 0.79f, bamper.gameObject.transform.localScale.z);
		shield_bamper.gameObject.transform.localScale = new Vector3 (1.2f, shield_bamper.gameObject.transform.localScale.y, shield_bamper.gameObject.transform.localScale.z);
	}

	private void resizeShield(bool decreate) {
		float tmpYsizeBamper = bamper.gameObject.transform.localScale.y;
		float tmpXsizeShield = shield_bamper.gameObject.transform.localScale.x;
		float incSize = 0.15f;
		if (decreate) {
			incSize *= -1f;
		}

		if (tmpYsizeBamper < MAX_SHIELD && tmpYsizeBamper > MIN_SHIELD) {
			if (tmpXsizeShield < MAX_SHIELD && tmpXsizeShield > MIN_SHIELD) {
				bamper.gameObject.transform.localScale += new Vector3 (0, incSize, 0f);
				shield_bamper.gameObject.transform.localScale += new Vector3 (incSize, 0, 0f);
			}
		}
	}

	private void BonusBombAction() {
		if (player.activeSelf) {
			player.GetComponent<Player>().DestroyPlayer();
		}
	}

	private void IncSpeed(GameObject _go) {
		//_go.transform.localScale = new Vector3 (MIN_SCALE, MIN_SCALE, MIN_SCALE);
	}

	private void DecreateSpeed(GameObject _go) {

	}

	private void UpSpeed() {
		ActionAllBalls(IncSpeed);
	}

	private void DownSpeed() {
		ActionAllBalls(DecreateSpeed);
	}

	private void AddLife() {
		CorrectLife.incLife();
	}

	private void SmallBall() {
		ActionAllBalls(ZipBall);
	}

	private void ResizeNornal() {
		ActionAllBalls(UnZipBall);
	}

	private void UnZipBall(GameObject _go) {
		_go.transform.localScale = new Vector3 (MAX_SCALE, MAX_SCALE, MAX_SCALE);
		_go.gameObject.tag = "Ball";
	}

	private void ZipBall(GameObject _go) {
		//Debug.Log ("ZIP: " + _go.transform.position);
		_go.transform.localScale = new Vector3 (MIN_SCALE, MIN_SCALE, MIN_SCALE);
		_go.gameObject.tag = "SmallBall";
	}

	private void Multiple3() {
		ActionAllBalls(GoAllMultiple3);
	}

	private void GoAllMultiple3(GameObject _go) {
		_go.GetComponent<CloneBall>().SetClone(3);
	}

	private void Multiple5() {
		ActionAllBalls(GoAllMultiple5);
	}
	
	private void GoAllMultiple5(GameObject _go) {
		_go.GetComponent<CloneBall>().SetClone(5);
	}

	private void ActionAllBalls(SelectMethod _deleghat) {
		object[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
		foreach(GameObject thisBall in allBalls) {
			_deleghat(thisBall);
		}
		object[] allBallsSmall = GameObject.FindGameObjectsWithTag("SmallBall");
		foreach(GameObject thisBall in allBallsSmall)
			_deleghat(thisBall);

		//if (((GameObject) thisBall).activeInHierarchy)
				//print(thisBall + " is an active object") ;
	}

	//test/

	public void AllBonusOffKaretka() {
		reurnShield();
	}

	void Update() {
		//resize test
		//SmallBall();
	}
}
