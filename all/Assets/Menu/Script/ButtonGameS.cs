using UnityEngine;
using System.Collections;

public class ButtonGameS : MonoBehaviour {

	//public GameObject fire_object;

	void OnMouseDown() {
		//Debug.Log ("PRESS " + gameObject.name);
		object[] allBalls;

		allBalls = GameObject.FindGameObjectsWithTag("Ball");
		ActionMethodPressDown(allBalls);

		allBalls = GameObject.FindGameObjectsWithTag("SmallBall");
		ActionMethodPressDown(allBalls);

	}

	private void ActionMethodPressDown(object[] _arrayObj) {
		foreach(GameObject thisBall in _arrayObj) {
			if (name == "go") { 
				thisBall.gameObject.GetComponent<BallV2>().pressedButtonGo();
			};
			if (name == "Pull") {
				thisBall.gameObject.GetComponent<BallV2>().preesedButtonMagnite();
			};
			if (name == "Fire") {
				FireAnimation.pressFire = true;
			};
		}
	}

	private void ActionMethodPressUp(object[] _arrayObj) {
		foreach(GameObject thisBall in _arrayObj) {
			if (name == "go") { 
				thisBall.gameObject.GetComponent<BallV2>().pressUpButtonGo();
			};
			if (name == "Pull") {
				thisBall.gameObject.GetComponent<BallV2>().pressUpButtonMagnite();
			};
			if (name == "Fire") {
				FireAnimation.pressFire = false;
			};
		}
	}

	void OnMouseUp() {
		//Debug.Log ("PRESS UP " + gameObject.name);
		object[] allBalls;
		allBalls = GameObject.FindGameObjectsWithTag("Ball");
		ActionMethodPressUp(allBalls);

		allBalls = GameObject.FindGameObjectsWithTag("SmallBall");
		ActionMethodPressUp(allBalls);
	}
}