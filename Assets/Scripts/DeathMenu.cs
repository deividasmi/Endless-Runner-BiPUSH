using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour {

	// Use this for initialization
	public string mainMenuLevel;

	public void RestartGame(){
		FindObjectOfType<GameManager> ().Reset ();
	}

	public void ReturnToMain(){
		Application.LoadLevel (mainMenuLevel);
	}


}
