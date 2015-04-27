using UnityEngine;
using System.Collections;

public class SelectStyleGeneral : MonoBehaviour {

	public StyleLevelBlock selectStyleLevelBlock;
	public static string tmpStyleLevelBlock;

	// Use this for initialization
	void Awake () {
		tmpStyleLevelBlock = selectStyleLevelBlock.ConfigSelection.ToString();
	}
}
