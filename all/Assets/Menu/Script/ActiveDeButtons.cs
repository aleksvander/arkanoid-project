using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveDeButtons : MonoBehaviour {

	private Vector2 begin_pos;
	public Vector2 end_pos;

	private Vector2 begin_rotate;
	//public Vector2 end_rotate;

	public float speed_move;

	public static bool statusFireButton = false;
	public static bool stopFireButton = true;

	private static List<bool> _stacks = new List<bool>();

	Transform _t;

	void Awake() {
		_t = transform;
	}

	void Start() {
		begin_pos = _t.position;
		StackActionForStatus (false);
	}

	public static void StackActionForStatus(bool _status) {
		_stacks.Add (_status);
		//Debug.Log (" STATUS " + _status);
	}

	private void RemoveStackLast() {
		_stacks.RemoveAt(0);
	}

	void Update() {
		//Debug.Log (" _stacks.Count  " + _stacks.Count);
		if (_stacks.Count == 0) {
			stopFireButton = false;
		} else {
			//Debug.Log(" _stacks[0] " + _stacks[0]);
			statusFireButton = _stacks[0];
			stopFireButton = true;
			//Debug.Log(" statusFireButton " + statusFireButton);
		}

		if (stopFireButton) {
			if (!statusFireButton) {
				MoveAndRotationDeactive ();
			} else {
				MoveAndRotationActive ();
			}
		}
		if (FireAnimation.ifFire = true) {
			//stopFireButton = true;
		}
	}

	void MoveAndRotationDeactive() {
		if (end_pos.x > _t.position.x ) {
			_t.Translate (((Vector2.right * -1f) * Time.deltaTime) / speed_move);
			_t.Rotate(Vector2.up * -1f, Time.deltaTime * 30f);
		} else {
			statusFireButton = true;
			RemoveStackLast();
		}
	}

	void MoveAndRotationActive() {
		if (begin_pos.x < _t.position.x ) {
			_t.Translate (((Vector2.right) * Time.deltaTime) / speed_move);
			_t.Rotate(Vector2.up, Time.deltaTime * 30f);
		} else {
			statusFireButton = false;
			RemoveStackLast();
		}
	}


}