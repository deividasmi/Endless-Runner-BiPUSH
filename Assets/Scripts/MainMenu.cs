using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public string playGameLevel;
	public string loginLevel;
	public Text userNameLabel;
	private UserData theUserData;

	void Start () {

		theUserData = FindObjectOfType<UserData> ();
		if (theUserData.isLogedIn) {
			userNameLabel.text = theUserData.userName;
		}
	}

	public void PlayGame(){
		Application.LoadLevel (playGameLevel);
	}

	public void QuitGame(){
		Application.Quit ();
	}



	public void Login(){
		Application.LoadLevel (loginLevel);
	}

}
