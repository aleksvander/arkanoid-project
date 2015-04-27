using UnityEngine;
using System.Collections;

public class CalculateScore : MonoBehaviour {

	private static int globalScore = 0;
	private static int procent = 0;
	public static float timer = 0;

	void Start() {
		globalScore = 0;
		procent = 0;
		timer = 0;
	}

	public static void correctPrice(int _price) {
		if (timer > 0) {
			globalScore += _price + procent;
			//Debug.Log (procent);
		} else {
			increateBonus();

			globalScore += _price;
		}
		setTimer();
	}

	public void Update() {
		timerBonus();

		guiText.text = "SCORE: " + globalScore;
	}

	static void setTimer() {
		timer = 3;
	}

	static void increateBonus() {
		if (timer > 0) 
		{
			if (procent == 0) 
			{
				procent = 10;
			} else {
				procent *= 2;
			}
		} else {
			procent = 0;
		}
//		Debug.Log ("procent: " + procent);
	}

	public void timerBonus() {
		if (timer > 0) { 
			timer -= Time.deltaTime;
		}
//		Debug.Log ("timer: " + timer);
	}
}
