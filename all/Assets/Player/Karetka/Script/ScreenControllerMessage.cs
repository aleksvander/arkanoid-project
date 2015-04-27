using UnityEngine;
using System.Collections;

public class ScreenControllerMessage : MonoBehaviour {

	public GameObject player;
	public GameObject guitext22;

	void OnMouseOver() {
		if (Player.isScreenTouch) {
			if (Application.platform == RuntimePlatform.Android) {
				MouseAction();
			} else {
				MouseAction();
			}
		}
	}

	void OnMouseExit() {
		Player.TurnOffAllFire();
	}

	void OnMouseUp() {
		Player.TurnOffAllFire();
	}

	private void TouchAction() {
		if(Input.touchCount > 0f) {
			Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;
			
			//Switch through touch event
			switch(Input.GetTouch(0).phase)
			{
			case TouchPhase.Began:
				break;
				
			case TouchPhase.Moved:
				//						DragObject(deltaPosition);
				Player.DragObject(deltaPosition);
				break;
				
			case TouchPhase.Ended:
				Player.TurnOffAllFire();
				break;
			}
		}
	}

	private void MouseAction() {
		if(Input.GetMouseButton(0)) {
			//Vector2 deltaPosition = Input.mousePosition; //_x - transform.position.x * speed * Time.deltaTime
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 deltaPosition = ray.origin + (ray.direction * (Camera.main.transform.position.z));
			deltaPosition.x *= -1f;
			//Debug.Log (deltaPosition);
			Player.DragObject(deltaPosition);
		} else {
			Player.TurnOffAllFire();
		}
	}

	void Update() {
		if (Player.isAccelerometer) {
			Vector3 dir = new Vector3 (0,0,0);
			if (Input.accelerationEventCount > 0) {
				dir.x = Input.acceleration.x;
				//Debug.Log (Input.accelerationEventCount);
				Player.AccelerometerAction(dir);
			}
		}
	}
}
