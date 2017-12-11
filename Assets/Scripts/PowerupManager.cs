using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour {


	private bool doublePoints;
	private bool safeMode;

	private bool powerupActive;
	private float powerUpLenghtCounter;

	private ScoreManager theScoreManager;
	private PlatformGenerator thePlatformGenerator;
	private float normalPointsPerSecond;
	private float spikeRate;
	private PlatformDestroyer[] spikeList;
	private GameManager theGameManager;

	// Use this for initialization
	void Start () {
		theScoreManager = FindObjectOfType<ScoreManager> ();
		thePlatformGenerator = FindObjectOfType<PlatformGenerator> ();
		theGameManager = FindObjectOfType<GameManager> ();
		normalPointsPerSecond = theScoreManager.pointsPerSecond;
	}
	
	// Update is called once per frame
	void Update () {
		if (powerupActive) {
			powerUpLenghtCounter -= Time.deltaTime;

			// resets after death
			if (theGameManager.powerUpReset) {
				powerUpLenghtCounter = 0;
				Debug.Log (theGameManager.powerUpReset);
				theGameManager.powerUpReset = false;
			}

			if (doublePoints) {
				theScoreManager.pointsPerSecond = normalPointsPerSecond * 2f;
				theScoreManager.shouldDouble = true;
			}

			if (safeMode) {
				thePlatformGenerator.randomSpikeTreshold = 0;
			}

			if (powerUpLenghtCounter <= 0) {
				theScoreManager.pointsPerSecond = normalPointsPerSecond;
				thePlatformGenerator.randomSpikeTreshold = spikeRate;
				theScoreManager.shouldDouble = false;
				powerupActive = false;
			}
		}
	}

	public void ActivePowerup(bool points, bool safe, float time){
		doublePoints = points;
		safeMode = safe;
		powerUpLenghtCounter = time;

		//normalPointsPerSecond = theScoreManager.pointsPerSecond;
		spikeRate = thePlatformGenerator.randomSpikeTreshold;
		if (safeMode){
			spikeList = FindObjectsOfType<PlatformDestroyer> ();
		 	for (int i = 0; i < spikeList.Length; i++) {
				if(spikeList[i].gameObject.name.Contains ("Spike")){
					spikeList [i].gameObject.SetActive (false);
				}
			}
		}
		powerupActive = true;

	}

}
