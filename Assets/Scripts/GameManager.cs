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


	//public ScrollingBackground background;
	//private Vector3 backgroundStartPoint;

	// Use this for initialization
	void Start () {
		platformStartPoint = platformGenerator.position;
		playerStartPoint = thePlayer.transform.position;
		theScoreManager = FindObjectOfType<ScoreManager> ();
		//backgroundStartPoint = background.transform.position;
		//Debug.Log (backgroundStartPoint.x + " ::: " + backgroundStartPoint.y);
		theBackgrounds = FindObjectsOfType<ScrollingBackground> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RestartGame (){

		theScoreManager.scoreIncreasing = false;
		thePlayer.gameObject.SetActive (false);
		theDeathMenu.gameObject.SetActive (true);
		//StartCoroutine ("RestartGameCo");
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
		theScoreManager.scoreCount = 0;
		theScoreManager.scoreIncreasing = true;
		time = DateTime.Now.ToString ("yyyy-MM-dd HH:mm:ss");
		powerUpReset = true;
		backroundReset = true;
		backgroundReset ();
		//Debug.Log (background.transform.position.x + " ::: " + background.transform.position.y);
		//background.transform.position = backgroundStartPoint;
		//Debug.Log (background.transform.position.x + " ::: " + background.transform.position.y);
	}

	private void backgroundReset(){
		for (int i = 0; i < theBackgrounds.Length; i++) {
			theBackgrounds [i].ResetBackground ();
		}
	}

	/*public IEnumerator RestartGameCo(){

		theScoreManager.scoreIncreasing = false;
		thePlayer.gameObject.SetActive (false);
		yield return new WaitForSeconds (0.5f);

		platformList = FindObjectsOfType<PlatformDestroyer> ();
		for (int i = 0; i < platformList.Length; i++) {
			platformList [i].gameObject.SetActive (false);
		}

		thePlayer.transform.position = playerStartPoint;
		platformGenerator.position = platformStartPoint;
		thePlayer.gameObject.SetActive (true);
		theScoreManager.scoreCount = 0;
		theScoreManager.scoreIncreasing = true;

	}*/
}
