using UnityEngine;
using System.Collections;

public class PodDestroy : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		//Debug.Log(col.tag);
		if (col.tag == "Exploit") {
			//DestroyObjectAndCalculatePrice();
			transform.parent.gameObject.GetComponent<DestroyBlock>().DestroyObjectAndCalculatePrice();
		} else if(col.tag == "Exploit" && tag != "ExploitBlock" || col.tag == "Shot") {
			if (col.tag != "Ball" && col.tag != "SmallBall") {
				if (col.tag == "Ball") {
					transform.parent.gameObject.GetComponent<DestroyBlock>().DecLife();
				} else if(col.tag == "SmallBall") {
					transform.parent.gameObject.GetComponent<DestroyBlock>().HalfDecLife();
				}

			}
		}
	}

	void OnCollisionEnter(Collision col) {
		//Debug.Log(col.gameObject.tag);
		if (tag != "ExploitBlock" && col.gameObject.tag != "Shot" && col.gameObject.tag !="Spark") {
			//call decreate life
			Debug.Log (col);
			if (col.gameObject.tag == "Ball") {
				transform.parent.gameObject.GetComponent<DestroyBlock>().DecLife();
			} else if(col.gameObject.tag == "SmallBall") {
				transform.parent.gameObject.GetComponent<DestroyBlock>().HalfDecLife();
			}
		}
	}
}
