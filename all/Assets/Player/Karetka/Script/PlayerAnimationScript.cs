using UnityEngine;
using System.Collections;

public class PlayerAnimationScript : MonoBehaviour {

	private Animator anim;
	public ControlParticleSystemLeft _leftFire;
	public ControlParticleSystemRight _rightFire;

	public bool isLeaft;
	public bool isRight;

	public bool isLeaftFull;
	private bool leaftTmpFull;
	public bool isRightFull;
	private bool rightTmpFull;

	//private bool stope = false;

	void Awake () {
		anim = GetComponent<Animator>();
		//_leftFire = GetComponent<ControlParticleSystemLeft>();
		//_rightFire = GetComponent<ControlParticleSystemRight>();

	}

	public void LeftAnimation() {
		anim.SetBool("isLeaft", true);
	}

	public void IdleAnimation() {
		anim.SetBool("isLeaft", false);
		anim.SetBool("isRight", false);
	}

	public void RightAnimation() {
		anim.SetBool("isRight", true);
	}
	// Use this for initialization
	/*void Start () {
		//anim.SetBool("isLeaft", true);
	}
	
	// Update is called once per frame
	void Update () {
		//anim.SetFloat("Speed", 100f);
	}*/

	void FixedUpdate() {
		if (isLeaft) {
			fleftFire(isLeaft);
		} else if (!isLeaft) {
			fleftFire(isLeaft);
		}

		if (isRight) {
			frightFire(isRight);
		} else if (!isRight) {
			frightFire(isRight);
		}

		//Debug.Log ("isLeaftFull " + isLeaftFull);

		if (isLeaftFull) {
			fleftFireFull(isLeaftFull);
			leaftTmpFull = true;
		} else if (!isLeaftFull && leaftTmpFull) {
			frighFireFull(true);
			leaftTmpFull = false;
		}

		if (isRightFull) {
			frighFireFull(isRightFull);
			rightTmpFull = true;
		} else if (!isRightFull && rightTmpFull) {
			fleftFireFull(true);
			rightTmpFull = false;
		}

		/*if (stope) {
			_leftFire.EnableFire(false);
			_leftFire.EnableFireFull(false);
			_rightFire.EnableFire(false);
			_rightFire.EnableFireFull(false);
		}*/
	}

	private void fleftFire(bool isLeaft) {
		if (_leftFire != null)
		_leftFire.EnableFire(isLeaft);
	}

	private void fleftFireFull(bool isLeaftFull) {
		//Debug.Log (isLeaftFull);
		if (_leftFire != null) 
		_leftFire.EnableFireFull(isLeaftFull);
	}

	private void frightFire(bool isRight) {
		if (_rightFire != null)
		_rightFire.EnableFire(isRight);
	}

	private void frighFireFull(bool isRightFull) {
		if (_rightFire != null)
		_rightFire.EnableFireFull(isRightFull);
	}

	public void AllStopAnimation() {
		IdleAnimation();
		isLeaft = false;
		isRight = false;
		isLeaftFull = false;
		isRightFull = false;
		leaftTmpFull = false;
		rightTmpFull = false;
		anim.SetBool("isAllFalse", true);

		//stope = true;
		//_rightFire.GetComponent<ParticleSystem>().isStopped = true;
		GetComponent<Animator>().enabled = false;
	}

	public void AllResumeAnimation() {
		GetComponent<Animator>().enabled = enabled;
		anim.SetBool("isAllFalse", false);
		//_leftFire.GetComponent<ParticleSystem>().enableEmission = false();
		//_rightFire.GetComponent<ParticleSystem>().enableEmission = false();
		//stope = false;
	}
}
