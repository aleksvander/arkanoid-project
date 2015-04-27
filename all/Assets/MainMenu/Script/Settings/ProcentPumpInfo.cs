using UnityEngine;
using System.Collections;

public class ProcentPumpInfo : MonoBehaviour {

	public static float DeCalculateProcent(int tmpI) {
				//float tmpF = Mathf.Round(transform.position.x * 100);
				//Debug.Log ("CHE ZA NAX?");
				float tmpF = 0;
				//int tmpI = 0;
				if (tmpI >= 0) {
						tmpF = -2.5f;
				}
		
				if (tmpI >= 10) {
						tmpF = -2f;
				}
		
				if (tmpI >= 20) {
						tmpF = -1.5f;
				}
		
				if (tmpI >= 30) {
						tmpF = -1f;
				}
		
				if (tmpI >= 40) {
						tmpF = -0.5f;
				}
		
				if (tmpI >= 50) {
						tmpF = -0f;
				}
		
				if (tmpI >= 60) {
						tmpF = 0.5f;
				}
		
				if (tmpI >= 70) {
						tmpF = 1f;
				}
		
				if (tmpI >= 80) {
						tmpF = 1.5f;
				}
		
				if (tmpI >= 90) {
						tmpF = 2f;
				}
		
				if (tmpI >= 100) {
						tmpF = 2.9f;
				}
		//Debug.Log ("tmpF " + tmpF);
				return tmpF;
		}

	public static int CalculateProcent(float tmpF) {
		tmpF = Mathf.Round(tmpF * 100);
		//Debug.Log (" tmpF " + tmpF);
		int tmpI = 0;
		if (tmpF >= -250) {
			tmpI = 0;
		}
		
		if (tmpF > -200) {
			tmpI = 10;
		}
		
		if (tmpF > -150) {
			tmpI = 20;
		}
		
		if (tmpF > -100) {
			tmpI = 30;
		}
		
		if (tmpF > -50) {
			tmpI = 40;
		}
		
		if (tmpF > 0) {
			tmpI = 50;
		}
		
		if (tmpF > 50) {
			tmpI = 60;
		}
		
		if (tmpF > 100) {
			tmpI = 70;
		}
		
		if (tmpF > 150) {
			tmpI = 80;
		}
		
		if (tmpF > 200) {
			tmpI = 90;
		}
		
		if (tmpF >= 250) {
			tmpI = 100;
		}
		//Debug.Log ("tmpI " + tmpI);
		return tmpI;
	}
}
