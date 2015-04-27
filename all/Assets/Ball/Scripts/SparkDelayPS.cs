using UnityEngine;
using System.Collections;

public class SparkDelayPS : MonoBehaviour {

	private ParticleSystem _p;
	private float timer = 0.2f;


	void Awake() {
		_p = particleSystem;
		//Поворачиваем в сторону столкновения

		//Включаем
		_p.Play();
	}

	void FixedUpdate() {
		//Удаляем по таймеру
		timer -= Time.deltaTime;
		if (timer < 0) Destroy(gameObject);
	}
}
