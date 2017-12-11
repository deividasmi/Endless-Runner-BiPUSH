using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour {


	public int userID;
	public string userName;
	public bool isTester;
	public bool isLogedIn;
	private LoginMenu theLoginMenu;


	// Use this for initialization
	public void SaveData () {
		theLoginMenu = FindObjectOfType<LoginMenu> ();
//		userID = theLoginMenu.userID;
		userName = theLoginMenu.userName;
		isTester = theLoginMenu.isTester;
		isLogedIn = theLoginMenu.isLogedIn;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
