using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public string playGameLevel;
	public string loginLevel;
	public Text userNameLabel;
    public Button LoginButton;
	private UserData theUserData;
	public GameObject theScoreMenu;
	public GameObject theMainMenu;
	public Text userNameLabel2;


	void Start () {
        try{
            theUserData = FindObjectOfType<UserData>();

            if (theUserData.isLogedIn) {
                userNameLabel.text = "Welcome " + theUserData.userName;
                userNameLabel2.text = theUserData.userName;
                LoginButton.interactable = false;
            }
        }
        catch
        {
            Debug.Log("User data is null. User probably skipped loggin, should do smth about that");
        }
    }

	public void OnClickScore(){
		theMainMenu.SetActive (false);
		theScoreMenu.SetActive (true);
	}


	public void OnClickBack(){
		theMainMenu.SetActive (true);
		theScoreMenu.SetActive (false);
	}

	public void PlayGame(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(playGameLevel);
	}

	public void QuitGame(){
		Application.Quit ();
	}



	public void Login(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(loginLevel);
	}



}
