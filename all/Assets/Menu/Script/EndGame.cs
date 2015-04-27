using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	private static bool isWin = false;
	private static bool isLose = false;
	public static bool GLOBAL_STATUS;
	private static float timer = 3f;


	void Start() {
		Time.timeScale = 1;
		allNull();
	}

	// Update is called once per frame
	public static void WinLoseAction (bool result) {
		if (result) {
			isWin = true;
			GLOBAL_STATUS = true;
			isLose = false;
		} else {
			isWin = false;
			GLOBAL_STATUS = false;
			isLose = true;
		}
	}

	public static void allNull() {
		isWin = false;
		isLose = false;
		GLOBAL_STATUS = false;
	}

	private void defaultActionWinlose() {
		Time.timeScale = 0;
		gameObject.GetComponent<MeshRenderer>().enabled = true;
		transform.position = new Vector3 (0, transform.position.y, transform.position.z);
	}

	private void win() {
		actionAllChild("Win");
	}

	private void lose() {
		actionAllChild("Lose");
	}

	private void actionAllChild(string isk) {
		foreach (Transform child in transform)
		{
			if (child.name == isk) {
				child.gameObject.GetComponent<MeshRenderer>().enabled = true;
			}
		}
	}

	void Update() {
		if (isWin) {
			defaultActionWinlose();
			win ();
		}

		if (isLose) {
			defaultActionWinlose();
			lose ();
		}

		timer -= Time.deltaTime;
		if (timer < 0) {
			Debug.Log ("TIME");
			object[] allBloks = GameObject.FindGameObjectsWithTag("Block");
			//ExploitBlock
			object[] allBloksExploids = GameObject.FindGameObjectsWithTag("ExploitBlock");
			if (allBloks.Length == 0 && allBloksExploids.Length == 0) EndGame.WinLoseAction(true);
			timer = 3f;
		}
	}
}
