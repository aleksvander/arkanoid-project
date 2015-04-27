using UnityEngine;
using System.Collections;

public class GlobalSpeed : MonoBehaviour {

	private static float globalSpeed = 5;
	private static float outTimerForNotCollisium = 0.3f;
	public static bool ignore_inc_speed = false;
	public static float timer_cheker = 2.0f;
	public static bool create_spark_logic = false;
	
	void Start() {
		globalSpeed = 5;
		outTimerForNotCollisium = 0.3f;
		ignore_inc_speed = false;
		timer_cheker = 2.0f;
		create_spark_logic = false;
	}

	public static float GetSetGlobalSpeed {
		get {
			return globalSpeed;
		}
		set {
			globalSpeed = value;
		}
	}

	public static void IncGlobalSpeed() {
		if (globalSpeed < 15) globalSpeed += 1f;
		RoundSpeed();
	}

	public static void DecGlobalSpeed() {
		if (globalSpeed > 3) globalSpeed -= 1f;
		RoundSpeed();
	}

	public static void IncreateSpeed() {
		if (!ignore_inc_speed)  {
			outTimerForNotCollisium -= Time.deltaTime;
			//timer_cheker = 0.2f;
		/*} else {
			timer_cheker -= Time.deltaTime;
			if (timer_cheker < 0) {
				Debug.Log (timer_cheker);
				ignore_inc_speed = false;
			//	create_spark_logic = false;
			}*/
		}

		if (outTimerForNotCollisium < 0) {
			GlobalSpeed.GetSetGlobalSpeed += 0.2f;
			//Debug.Log (globalSpeed / 22f);
			outTimerForNotCollisium = 0.3f + (globalSpeed / 10f); //increate timer в зависимости от скорости, чем выше тем медленее
		}

		RoundSpeed();
	}

	void Update() {
		if (GlobalSpeed.create_spark_logic) {
			timer_cheker -= Time.deltaTime;
			GlobalSpeed.ignore_inc_speed = true;
		}
		if (timer_cheker < 0) {
			GlobalSpeed.create_spark_logic = false;
			GlobalSpeed.ignore_inc_speed = false;
			timer_cheker = 2f;
		}
	}

	private static void RoundSpeed() {
		globalSpeed = (Mathf.Round(globalSpeed * 100f) / 100f);
	}
}
