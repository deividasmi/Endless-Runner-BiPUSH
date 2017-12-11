using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public string mainMenuLevel;
	public GameObject pauseMenu;
	public GameObject reportMenu;
	public Button theReportButton;
	private UserData theUserData;

	public void Start(){
        try
        {
            theUserData = FindObjectOfType<UserData>();
            if (theUserData.isTester)
            {
                theReportButton.interactable = true;
            }
            else
            {
                theReportButton.interactable = false;
            }
        }
        catch
        {
            Debug.Log("User Data not found. User probably skipped login");
        }
		
	}

	public void PauseGame(){
		Time.timeScale = 0f;
		pauseMenu.SetActive (true);
	}

	public void ReportBug(){
		pauseMenu.SetActive (false);
		reportMenu.SetActive (true);
	}

	public void ResumeGame(){
		Time.timeScale = 1f;
		pauseMenu.SetActive (false);
	}

	public void RestartGame(){
		Time.timeScale = 1f;
		pauseMenu.SetActive (false);
		FindObjectOfType<GameManager> ().Reset ();
	}

	public void ReturnToMain(){
		if (FindObjectOfType<UserData> ()) {
			FindObjectOfType<GameManager> ().SendScore ();
		}
		Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuLevel);
	}



}
