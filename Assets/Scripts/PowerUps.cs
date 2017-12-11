using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {

	public bool doublePoints;
	public bool safeMode;

	public float powerupLenght;
	private PowerupManager thePowerupManager;
	public Sprite[] powerUpSprites;

	// Use this for initialization
	void Start () {
		thePowerupManager = FindObjectOfType<PowerupManager> ();
	}

	void Awake(){
		int powerUpSelector = Random.Range (0, 2);
		switch (powerUpSelector) {
		case 0:
			doublePoints = true;
			break;
		case 1:
			safeMode = true;
			break;
		}
		GetComponent<SpriteRenderer> ().sprite = powerUpSprites[powerUpSelector];

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Player") {
			thePowerupManager.ActivePowerup (doublePoints,safeMode,powerupLenght);
		}
		gameObject.SetActive (false);
	}
}
