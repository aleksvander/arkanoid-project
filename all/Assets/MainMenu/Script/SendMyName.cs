using UnityEngine;
using System.Collections;

public class SendMyName : MonoBehaviour {
	
	public GameObject NameObjectToSend;
	private string NameThisObject;

	void OnMouseUp() {
		//Debug.Log ("CLICK");
		NameObjectToSend.GetComponent<ActionButtonsKey> ().SelectAction (this.gameObject.name);
	}
}
