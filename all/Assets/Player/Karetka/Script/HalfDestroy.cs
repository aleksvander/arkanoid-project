using UnityEngine;
using System.Collections;

public class HalfDestroy : MonoBehaviour {

	void OnTriggerEnter(Collider col) {

		if (col.tag == "Ball" || col.tag == "SmallBall") {
			//Debug.Log (col);
			transform.parent.gameObject.GetComponent<DestroyBlock>().HalfDestroyNow();
		}

		if(col.tag == "Exploit" && tag != "ExploitBlock" || col.tag == "Shot") {
//			Debug.Log (col);
			transform.parent.gameObject.GetComponent<DestroyBlock>().HalfDestroyNow();
		}
	}
	
	void OnCollisionEnter(Collision col) {
		if (tag != "ExploitBlock" && col.gameObject.tag != "Shot") {
			Debug.Log (col.gameObject.name);
			if (col.gameObject.tag !="Spark") {
				transform.parent.gameObject.GetComponent<DestroyBlock>().HalfDestroyNow();
			}
		}
	}
}
