using UnityEngine;
using System.Collections;

public class ControlParticleSystemLeft : MonoBehaviour {

	private ParticleSystem _p;

	void Awake() {
		_p = particleSystem;
		_p.playbackSpeed = 7f;
		_p.playOnAwake = false;
		_p.Stop();
	}
	
	public void EnableFire(bool _b) {
		if (_b) {
			_p.startLifetime = 1.5f;
			if (Application.loadedLevelName == "MainMenu") {
				_p.startLifetime = 6f;
			}
			_p.Play();
		} else if (!_b) {
			_p.Stop();
		}
	}

	public void EnableFireFull(bool _b) {
		if (_b) {
			_p.startLifetime = 3f;
			_p.Play();
		} else if (!_b) {
			_p.Stop();
		}
	}
}
