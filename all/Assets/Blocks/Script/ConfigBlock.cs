using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class ConfigBlock : MonoBehaviour  {

	public ListConfigBlock SelectedSkin;
	public StyleLevelBlock selectStyleLevelBlock;
	public static string tmpStyleBlockStatic;
	private string tmpStyleBlock;
	private bool isNapalm = false;

    //private bool isStart = true;

	public void Start () {
		//Debug.Log ((int) SelectedSkin.ConfigSelection);
		int i = (int) SelectedSkin.ConfigSelection;
		switch (i) // так же надо устанавливать цену... и брать ее из Price.cs
		{ 
		case 1:
			Debug.Log("Case 1");
			break;
		case 2:
			Debug.Log("Case 2");
			break;
		case 5:
			isNapalm = true;
			break;
		case 6:
			isNapalm = true;
			break;
		case 9:
//			Debug.Log ("Case 9");
			break;
		default:
//			Debug.Log("Default case");
			break;
		}
		//GameObject objMat = GameObject.Find ("/game/Blocks/Materials/mega_simple");
		//Debug.Log (objMat);
		if (tmpStyleBlockStatic == null) { 
			tmpStyleBlockStatic = SelectStyleGeneral.tmpStyleLevelBlock;
			Debug.Log(tmpStyleBlockStatic);
		}
		if (tmpStyleBlockStatic == null) { 
			tmpStyleBlock = selectStyleLevelBlock.ConfigSelection.ToString();
		} else {
			tmpStyleBlock = tmpStyleBlockStatic;
		}
		if (tmpStyleBlock == null) {
			Debug.Log ("ISKLUCHAUSHA SITUACIA!!!!!!!!!!!!!!!!!! SKASHI RAZRABU!!!!!!!!!!!");
			tmpStyleBlock = "mega";
		}

		//Material tmpMaterial = Resources.Load("Blocks/" + tmpStyleBlock + "/Materials/" + SelectedSkin.ConfigSelection.ToString(), typeof(Material)) as Material;
		Material tmpMaterial = Resources.Load("Blocks/mega/Materials/" + SelectedSkin.ConfigSelection.ToString(), typeof(Material)) as Material;
		renderer.material = tmpMaterial;
		//Debug.Log (i);
		if (isNapalm) {
			//capsuleCollider = GameObject.GetComponent(<CapsuleCollider>);
			//capsuleCollider-.enabled = true;
		}

        //isStart = true;
	}

	public void Update () {
		//if (!isStart) pozStart();
		//if (menuOption == "player") {

		//Debug.Log (SelectedSkin.ConfigSelection.ToString());
		if (isNapalm) ConfigNapalm();
	}

	private void ConfigNapalm() {
		EnableCapsuleCollider();	
		EnableScriptForExploid();
		setStatusExploidToAllScript();
		//DisableScriptDestroy();
		isNapalm = false;
	}

	private void setStatusExploidToAllScript() {
		//ExploidBlock_action.isExploid = true;
		//DestroyBlock.isExploid = true;
	}

	private void EnableCapsuleCollider() {
		gameObject.GetComponent<CapsuleCollider>().enabled = true;
	}
	

	private void EnableScriptForExploid() {
		gameObject.GetComponent<ExploidBlock_action>().enabled = true;
	}

	private void DisableScriptDestroy() {
		//gameObject.GetComponent<DestroyBlock>().enabled = false;
	}
}
