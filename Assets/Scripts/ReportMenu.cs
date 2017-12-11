using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ReportMenu : MonoBehaviour {

	public GameObject pauseMenu;
	public GameObject reportMenu;
	private UserData theUserData;
	private string time;
	//private string reportURL = "http://localhost/Endless_Runner/sendReport.php";
    private string reportURL = "http://193.219.91.103:3089/sendReport.php";
    public InputField message;
	public Text statusText;

	public void BackToPauseMenu(){
		reportMenu.SetActive (false);
		pauseMenu.SetActive (true);
	}



	public void OnClickSend(){
		theUserData = FindObjectOfType<UserData> ();
		StartCoroutine (SendReport (theUserData.userID ,message.text));
	}

	IEnumerator SendReport(int id, string message){
		WWWForm form = new WWWForm ();
		form.AddField ("idPost", id);
		form.AddField ("messagePost",message);
		time = DateTime.Now.ToString ("yyyy-mm-dd HH:mm:ss");
		WWW www = new WWW (reportURL, form);
		yield return www;
		statusText.text = "Message Sent";
		Debug.Log (www.text);
        Debug.Log(time);
	}


}
