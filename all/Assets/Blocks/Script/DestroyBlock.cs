using UnityEngine;
using System.Collections;

public class DestroyBlock : MonoBehaviour {
	
	public bool exploid_action = false;
	public GameObject prefabExploit;
	private float lifeBlock = 100f;

	private float timer_exploid = 0.05f;
	private Collision _col;
	public bool isExploid;

	private float halfTimer = 0.2f;
	private bool halfSwitcher = false;
	private bool halfDestroy = false;

	private int simple = 10;
	private int exploid = 15;
    //private int bonus = 25;
	private int use_price = 0;

	public bool block_is_3 = false;

	void Start() {
		//ExploidBlock_action eBA = new ExploidBlock_action();
		//Debug.Log (eBA.getStatusExploid());
		//isExploid = ExploidBlock_action;
		if (tag == "Block") lifeBlock = 1f;

		simple = Price.SIMPLE;
		exploid = Price.EXPLOID;
        //bonus = Price.BONUS;
		use_price = simple;

		if (block_is_3) lifeBlock = 3;
	}

	public void setBoolExploid() {
		exploid_action = true;
	}

	void Update() {
		halfTimer -= Time.deltaTime;
		if (halfTimer <= 0) {
			if (halfSwitcher) halfDestroy = false;
			halfSwitcher = false;
		}
//		if (halfDestroy) Debug.Log (halfDestroy);
		if (halfDestroy && !halfSwitcher && halfTimer < 0) DestroyObjectAndCalculatePrice();
	}

	void FixedUpdate() {
		if (lifeBlock <= 0 ) DestroyObjectAndCalculatePrice();
		if (exploid_action) {
			timer_exploid -= Time.deltaTime;
			if (timer_exploid <= 0) {
				if (exploid_action) {
					GameObject ObjectPrefabExploit = Instantiate(prefabExploit) as GameObject;
					ObjectPrefabExploit.transform.position = transform.position;
				} 
				if (timer_exploid < 0) {
					//Debug.Log (timer_exploid);
					DestroyObjectAndCalculatePrice();
				}
			}
		}
	}

	void OnTriggerEnter(Collider col) {
//		Debug.Log (col.name);
		if (tag != "FullArmor" && tag != "HalfArmor") {
			if(col.tag == "Exploit" && tag != "ExploitBlock" || col.tag == "Shot" && !exploid_action) {
				if (col.tag != "Ball" && col.tag != "SmallBall") {
					//if (col.tag !="Shot") {
//						Debug.Log ("DESTROY " + col.tag);
					if (!block_is_3) {
						DestroyObjectAndCalculatePrice();
					}
					//}
				}
			} else if(col.tag != "Exploit" || col.tag != "Shot" && !exploid_action) {

			}
		}
		if (tag == "FullArmor") {
			if (col.tag == "Exploit") {
				DestroyObjectAndCalculatePrice();
			} else if(col.tag == "Exploit" && tag != "ExploitBlock" || col.tag == "Shot" && !exploid_action) {
				if (col.tag != "Ball" && col.tag != "SmallBall") {
					if (col.tag == "Ball") {
						lifeBlock -= 1f;
					} else if(col.tag == "SmallBall") {
						lifeBlock -= 0.2f;
					}
					if (lifeBlock <= 0) {
						DestroyObjectAndCalculatePrice();
					}
				}
			}
		}
		if (tag == "HalfArmor") {
			if (col.tag == "Exploit") {
				DestroyObjectAndCalculatePrice();
			}
		}

		if (block_is_3) {
			if (col.tag == "Exploit") {
				DestroyObjectAndCalculatePrice();
			} else if(col.tag == "Exploit" && tag != "ExploitBlock" || col.tag == "Shot" && !exploid_action) {
				if (col.tag == "Ball") {
					lifeBlock -= 1f;
				} else if(col.tag == "SmallBall") {
					lifeBlock -= 0.2f;
				}
				//Debug.Log(lifeBlock);
				SkinSelect3Sec(lifeBlock);
				if (lifeBlock <= 0) {
					DestroyObjectAndCalculatePrice();
				}
			}
		}
	}
	

	void OnCollisionEnter(Collision col) {
		if (tag != "FullArmor" && tag != "HalfArmor") {
			if (tag != "ExploitBlock" && col.gameObject.tag != "Shot") {
				if (col.gameObject.tag !="Spark") {
					if (!block_is_3) {
						DestroyObjectAndCalculatePrice();
					}
				}
			}
		}
		if (tag == "FullArmor") {
			if (tag != "ExploitBlock" && col.gameObject.tag != "Shot" && col.gameObject.tag !="Spark") {
				if (col.gameObject.tag == "Ball") {
					lifeBlock -= 1f;
				} else if(col.gameObject.tag == "SmallBall") {
					lifeBlock -= 0.2f;
				}
				if (lifeBlock <= 0) {
					DestroyObjectAndCalculatePrice();
				}
			}
		}
		if (tag == "HalfArmor") {
			
		}
		if (block_is_3) {
			if (tag != "ExploitBlock" && col.gameObject.tag != "Shot" && col.gameObject.tag !="Spark") {
				if (col.gameObject.tag == "Ball") {
					lifeBlock -= 1f;
				} else if(col.gameObject.tag == "SmallBall") {
					lifeBlock -= 0.2f;
				}
				//Debug.Log(lifeBlock);
				SkinSelect3Sec(lifeBlock);
				if (lifeBlock <= 0) {
					DestroyObjectAndCalculatePrice();
				}
			}
		}
	}

	private void SkinSelect3Sec(float tmp) {
		if (tmp <= 2 && tmp >= 1) {
			Material tmpMaterial = Resources.Load("Blocks/Mega/Materials/mega_3_section1") as Material;
			renderer.material = tmpMaterial;
		} else if (tmp <= 1) {
			Material tmpMaterial = Resources.Load("Blocks/Mega/Materials/mega_3_section2") as Material;
			renderer.material = tmpMaterial;
		}

	}

	public void DecLife() {
		lifeBlock -= 1f;
		//Debug.Log (lifeBlock);
		halfSwitcher = true;
		halfTimer = 0.2f;
//		Debug.Log ("DEC LIFE");
	}

	public void HalfDecLife() {
		lifeBlock -= 0.5f;
		//Debug.Log (lifeBlock);
		halfSwitcher = true;
		halfTimer = 0.2f;
		//		Debug.Log ("DEC LIFE");
	}

	public void HalfDestroyNow() {
		//if (!halfSwitcher) {
//		Debug.Log ("HALF DESTROY NOW");
		halfDestroy = true;
		//halfTimer = 0.2f;
		//}
	}

	public void DestroyObjectAndCalculatePrice() {
		if (exploid_action) {
			use_price = simple;
		} else {
			use_price = exploid;
		}
		//create bonus
		//Debug.Log (GetComponent<BonusDrop>().getStatus());
		if (GetComponent<BonusDrop>().GetStatus()) {
			GetComponent<BonusDrop>().CreateBonus();
		}
		//Debug.Log ("DESTROY");
		//calculate price
		CalculateScore.correctPrice(use_price);

		//Destroy object
		Destroy(gameObject);
	}

	void SetUpScore() {

	}
}
