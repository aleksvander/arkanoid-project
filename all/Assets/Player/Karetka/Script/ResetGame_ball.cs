using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResetGame_ball : MonoBehaviour {

	public GameObject createBall;
	private float timer = 0.5f;

	void Update() {
		//foreach (GameObject forBall in GameObject.FindGameObjectsWithTag("Ball")) {

			//Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation) as GameObject;
		//}
		if (GameObject.FindGameObjectsWithTag("Ball").Length == 0 && GameObject.FindGameObjectsWithTag("SmallBall").Length == 0) {
			//Говорим прощай жизнь и создаем мячик


			timer -= Time.deltaTime;
			if (timer < 0) {
				if (CorrectLife.getLife() > 0) {
					GameObject newBall = Instantiate(createBall) as GameObject;
					newBall.gameObject.tag = "Ball";
					newBall.gameObject.GetComponent<BallV2>().isResetBall = true;
					//Выводим надпись предупреждающую

					CorrectLife.decLife();
				} else {
					//Сообщаем что игра закончена и предлагаем переиграть... т.е. выводим меню
					EndGame.WinLoseAction(false);
				}

				timer = 0.5f;
			}
		}
	}
}
