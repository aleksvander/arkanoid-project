using UnityEngine;
using System.Collections;

public class CorrectLife : MonoBehaviour {

	private static int life = 3;

	void Start() {
		life = 3;
	}

	public void setLife(int _life) {
		life = _life;
	}

	public static int getLife() {
		return life;
	}

	public static void incLife() {
		life += 1;
		//Debug.Log ("+1");
	}

	public static void decLife() {
		life -= 1;
		//Debug.Log ("-1");
	}

	// Update is called once per frame
	void Update () {
		guiText.text = "LIFE:      " + life;
		if (life < 0) EndGame.WinLoseAction(false);
	}
}
