using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReportMenu : MonoBehaviour {

	public GameObject pauseMenu;
	public GameObject reportMenu;
	private UserData theUserData;
	private string time;
	private string reportURL = "http://localhost/Endless_Runner/report.php";

	public void BackToPauseMenu(){
		reportMenu.SetActive (false);
		pauseMenu.SetActive (true);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SendReport(int id, string message){
		WWWForm form = new WWWForm ();
		form.AddField ("namePost", name);
		form.AddField ("messagePost",message);
		time = DateTime.Now.ToString ("yyyy-MM-dd HH:mm:ss");
		WWW www = new WWW (reportURL, form);
		yield return www;

		//Debug.Log (www.text);
	}


}
