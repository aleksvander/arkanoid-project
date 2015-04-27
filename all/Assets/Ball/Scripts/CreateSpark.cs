using UnityEngine;
using System.Collections;

public class CreateSpark : MonoBehaviour {

	public GameObject sparkPatrical;
	//public bool access = false;
	
	void OnCollisionEnter (Collision col) {
		if (!GlobalSpeed.create_spark_logic) {
			if (col.gameObject.tag != "Shot") {
				GameObject newObject = Instantiate(sparkPatrical) as GameObject;
				newObject.transform.position = transform.position;
				newObject.tag = "Spark";
			}
		} else {
			//access = true;
		}
		//newObject.transform.rotation = Quaternion.Euler(10, 0, 0);
		//Debug.Log(col.contacts[0].normal);

	}
}
