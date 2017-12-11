using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

	public Transform platformGenerator;
	private Vector3 platformStartPoint;

	public PlayerControler thePlayer;
	private Vector3 playerStartPoint;

	private PlatformDestroyer[] platformList;
	private ScoreManager theScoreManager;
	private UserData theUserData;

	public DeathMenu theDeathMenu;
	public bool powerUpReset;
	public bool backroundReset;
	public ScrollingBackground[] theBackgrounds;
	private string time; 
	//private string scoredbURL = "http://localhost/Endless_Runner/commitScore.php";
    private string scoredbURL = "http://193.219.91.103:3089/commitScore.php";



    void Start () {
		platformStartPoint = platformGenerator.position;
		playerStartPoint = thePlayer.transform.position;
		theScoreManager = FindObjectOfType<ScoreManager> ();
		theBackgrounds = FindObjectsOfType<ScrollingBackground> ();
		theUserData = FindObjectOfType<UserData> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RestartGame (){

		theScoreManager.scoreIncreasing = false;
		thePlayer.gameObject.SetActive (false);
		theDeathMenu.gameObject.SetActive (true);
	}

	public void Reset(){

		theDeathMenu.gameObject.SetActive (false);
		platformList = FindObjectsOfType<PlatformDestroyer> ();
		for (int i = 0; i < platformList.Length; i++) {
			platformList [i].gameObject.SetActive (false);
		}

		thePlayer.transform.position = playerStartPoint;
		platformGenerator.position = platformStartPoint;
		thePlayer.gameObject.SetActive (true);
		backroundReset = true;
		backgroundReset ();
		SendScore ();
		theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncreasing = true;
		time = DateTime.Now.ToString ("yyyy-MM-dd HH:mm:ss");
		powerUpReset = true;

	
	}

	private void backgroundReset(){
		for (int i = 0; i < theBackgrounds.Length; i++) {
			theBackgrounds [i].ResetBackground ();
		}
	}

	public void SendScore(){
        try
        {
            if(theUserData.isLogedIn){
			    int score = (int)Mathf.Round (theScoreManager.scoreCount);
			    StartCoroutine (CommitScore(theUserData.userID, score));
		    }
        }
        catch
        {
            Debug.Log("User data is null, user probably skipped loggin");
        }
		
	
	}

	IEnumerator CommitScore (int id, int score){
		WWWForm form = new WWWForm ();
		form.AddField ("idPost", id);
		form.AddField ("scorePost",score);
		time = DateTime.Now.ToString ("yyyy-mm-dd HH:mm:ss");
		WWW www = new WWW (scoredbURL, form);
		yield return www;
        Debug.Log(www.text);
        Debug.Log(time);
	}



}
