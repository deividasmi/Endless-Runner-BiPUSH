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
	//private string loginURL = "http://localhost/Endless_Runner/loginUser.php";
    private string loginURL = "http://193.219.91.103:3089/loginUser.php";
    //private string userDataURL = "http://localhost/Endless_Runner/userData.php";
    private string userDataURL = "http://193.219.91.103:3089/userData.php";
    public bool isLogedIn;
	public bool isTester;
	public int userID;
	public string userName;
	private UserData theUserData;

    public void Start()
    {
        theUserData = FindObjectOfType<UserData>();
    }

	public void SkipMenu(){
        //Loadina scena, taip pat kaip application.load tik sitas metodas nedeprecated ir warningu nemes
		UnityEngine.SceneManagement.SceneManager.LoadScene (mainMenuLevel);
		theUserData.SaveData ();
		DontDestroyOnLoad (theUserData);
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
		theUserData.SaveData ();
		DontDestroyOnLoad (theUserData);
        stateText.text = "Logging in... Please wait";
        Invoke("logInState",1f);
		

	}
	void logInState() {
		if (isLogedIn) {
			SkipMenu ();
		}
	}

	IEnumerator GetUserData(string name, string password){
		WWWForm form = new WWWForm ();
		form.AddField ("namePost", name);
		form.AddField ("passwordPost",password);
		WWW www = new WWW (userDataURL, form);
		yield return www;
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
		if(www.text == "Login success"){
            stateText.color = Color.green;
			isLogedIn = true;
			userName = name;
		}else{
			isLogedIn = false;
            stateText.color = Color.red;
		}
       

    }

}
