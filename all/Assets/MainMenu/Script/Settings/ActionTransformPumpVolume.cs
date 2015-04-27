using UnityEngine;
using System.Collections;

public class ActionTransformPumpVolume : MonoBehaviour {
	
	public static float _posKar_x;

	private float Y_POSITION;
	private float Z_POSITION;

	[SerializeField]
	private static float horizontallimit_m = 1.95f; 

	private Transform cachedTransform;

	private Vector3 startingPos;

	private void Start() {
		Y_POSITION = transform.position.y;
		Z_POSITION = transform.position.z;
		//Make reference to transform
		cachedTransform = transform;
		
		//Save start position
		startingPos = cachedTransform.position;
		//Debug.Log (transform.localPosition);
	}


	public void TransforMe (Vector3 deltaPosition) {
		_posKar_x = transform.position.x;

		int limit_x = 3;
		if (deltaPosition.x < 0) limit_x *= -1;
		float tmpF = 1f;
		//if (_posKar_x < 0f && deltaPosition.x < 0f && Mathf.Round(_posKar_x * 100f) < Mathf.Round(deltaPosition.x * 100f) + limit_x) tmpF = -1f;
		//if (_posKar_x > 0f && deltaPosition.x > 0f && Mathf.Round(_posKar_x * 100f) > Mathf.Round(deltaPosition.x * 100f) + limit_x) tmpF = -1f;
		//tmpF = deltaPosition.x;
		MoveAction (deltaPosition * -1f);
	}

	private void MoveAction(Vector3 deltaPosition) {
		/*float tmpX = 1f;
		tmpX = horizontallimit_m / 4f;
		if (deltaPosition.x < 0) tmpX *= -1f;
		
		cachedTransform.position = new Vector3(Mathf.Clamp((tmpX * dragspeed_m * tmpF) + (cachedTransform.position.x + 0f), 
		                                                   startingPos.x - horizontallimit_m, 
		                                                   startingPos.x + horizontallimit_m), 
		                                       Y_POSITION, Z_POSITION);
		Debug.Log (" deltaPosition.x " + deltaPosition.x);*/
		if (deltaPosition.x > 0 && deltaPosition.x > horizontallimit_m)
						deltaPosition.x = horizontallimit_m;
		if (deltaPosition.x < 0 && deltaPosition.x < horizontallimit_m * -1f)
			deltaPosition.x = horizontallimit_m * -1f;
		cachedTransform.position = new Vector3 (deltaPosition.x, Y_POSITION, Z_POSITION);
	}
	
	public void TransformCurrentPump(float _x) {
		transform.localPosition = new Vector3 (_x, transform.localPosition.y, transform.localPosition.z);
	}
}
