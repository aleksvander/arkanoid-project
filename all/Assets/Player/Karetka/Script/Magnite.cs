using UnityEngine;
using System.Collections;

public class Magnite : MonoBehaviour {

	public GameObject target;
	private LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		lineRenderer.SetPosition(0, this.transform.position);

		for(int i = 1; i < 4; i++) {
			Vector3 pos = Vector3.Lerp(this.transform.position,target.transform.position, i/1.0f);

			pos.x += Random.Range(-0.4f, 0.4f);
			pos.y += Random.Range(-0.4f, 0.4f);

			lineRenderer.SetPosition(i, pos);
		}

		lineRenderer.SetPosition(4, target.transform.position);
	}
}
