using UnityEngine;
using System.Collections;

public class ExploidBlock_action : MonoBehaviour {

	//public bool isExploid;

	/*void Awake() {

	}

	/*public void setExploid() {
		isExploid = true;
	}*/

	//Для взрывающегося блока или любого другого кто задевает остальное
	void OnTriggerEnter(Collider col) {

		if(col.tag == "Ball" || col.tag == "SmallBall" || col.tag == "Exploit" || col.tag == "Shot") {
			//Debug.Log (col);
			if (col.tag != "Ball" && col.tag != "SmallBall") gameObject.GetComponent<DestroyBlock>().setBoolExploid();
		}

		/*if (col.tag == "Block") {
			if (isExploid) {
				col.gameObject.GetComponent<ExploidBlock_action>().setExploid();
				//ПОЧИНИТЬ - ВСЕ ДЕЛО В ТОМ ЧТО ЭТА СОБАКА РЕАГИРУЕТ НА ТРИГГЕРЫ Т.Е, МНЕ НУЖНО ТРИГЕРЫ ОСТАВИТЬ ВКЛЮЧЕННЫМ НА ПОСТОЯНКУ... 
				//И УЖЕ ОБРАБАТЫВАТЬ СОБЫТИЯ
				//gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
				col.GetComponent<DestroyBlock>().setBoolExploid();
			}
		}
		Debug.Log (col.name);
		//	gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
		
		//	exploid_action = true;*/
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Ball" || col.gameObject.tag == "SmallBall") { 
			gameObject.GetComponent<DestroyBlock>().setBoolExploid();
		}
	}

	void FixedUpdate() {
		//if (gameObject.GetComponent<CapsuleCollider>().isTrigger == true) {
			//Destroy(gameObject);
		//}
	}

		
	private void ResizeCapsuleCollider() {
		gameObject.GetComponent<CapsuleCollider>().radius = 0.3f;
		gameObject.GetComponent<CapsuleCollider>().height = 0.9f;
	}
}
