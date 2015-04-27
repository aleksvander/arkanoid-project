using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour {
	
	// Update is called once per frame
	public static void restartScene () {
		Application.LoadLevel(Application.loadedLevelName);
	}
}
