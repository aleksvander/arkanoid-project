using UnityEngine;
using System.Collections;

public class ControlPumpVolume : MonoBehaviour {

	public GameObject pump;
	public GameObject procentText;
	
	void OnMouseOver() {
		if (Input.GetMouseButton(0)) {
			MouseAction();
		}
		/*if (Player.isScreenTouch) {
			if (Application.platform == RuntimePlatform.Android) {
				MouseAction();
			} else {
				MouseAction();
			}
		}*/
	}

	private void MouseAction() {
		if(Input.GetMouseButton(0)) {
			//Vector2 deltaPosition = Input.mousePosition; //_x - transform.position.x * speed * Time.deltaTime
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 deltaPosition = ray.origin + (ray.direction * (Camera.main.transform.position.z));
			deltaPosition.x *= -1f;
			pump.gameObject.GetComponent<ActionTransformPumpVolume>().TransforMe(deltaPosition);
		}
		procentText.GetComponent<ProcentUpdateVolume>().TextUpdate();
	}

	public void SendToTransformPump(float _percent) {
		pump.gameObject.GetComponent<ActionTransformPumpVolume>().TransformCurrentPump(_percent);
	}
}
