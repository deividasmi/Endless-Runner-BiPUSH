using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour {

	public string mainMenuLevel;
	public GameObject registrationMenu;
	public GameObject loginMenu;
	public InputField inputName;
	public InputField inputPassword;
	public Text stateText;
	private string loginURL = "http://localhost/Endless_Runner/loginUser.php";
	private string userDataURL = "http://localhost/Endless_Runner/userData.php";
	public bool isLogedIn;
	public bool isTester;
	public int userID;
	public string userName;
	private UserData theUserData;


	public void SkipMenu(){
		Application.LoadLevel (mainMenuLevel);
	}

	public void RegistrationMenu (){
		loginMenu.SetActive (false);
		registrationMenu.SetActive (true);
	}

	public void BackToLogin(){
		registrationMenu.SetActive (false);
		loginMenu.SetActive (true);
	}

	public void onClickLogind(){
		StartCoroutine (LoginUser (inputName.text,inputPassword.text));
		StartCoroutine (GetUserData (inputName.text, inputPassword.text));
		theUserData = FindObjectOfType<UserData> ();
		theUserData.SaveData ();
		DontDestroyOnLoad (theUserData);
		if (isLogedIn) {
			SkipMenu ();
		}

	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator GetUserData(string name, string password){
		WWWForm form = new WWWForm ();
		form.AddField ("namePost", name);
		form.AddField ("passwordPost",password);
		WWW www = new WWW (userDataURL, form);
		yield return www;
		//Debug.Log (www.text);
		parseData (www.text);
	}

	private void parseData(string dataString){
		string[] data = dataString.Split ('|');
		userID = int.Parse(data [0]);
		if(data[1] == "0"){
			isTester = false;
		}else{
			isTester = true;
		}
	}

	IEnumerator LoginUser(string name, string password){
		WWWForm form = new WWWForm ();
		form.AddField ("namePost", name);
		form.AddField ("passwordPost",password);

		WWW www = new WWW (loginURL, form);
		yield return www;
		stateText.text = www.text;
		//Debug.Log (www.text);
		if(www.text == "Login success"){
			isLogedIn = true;
			userName = name;
		}else{
			isLogedIn = false;
		}

	}

}
