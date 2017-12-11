using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Registration : MonoBehaviour {

	//private string registrationURL = "http://localhost/Endless_Runner/registration.php";
    private string registrationURL = "http://193.219.91.103:3089/registration.php";
    public InputField UserName;
	public InputField password;
	public InputField repeatPassword;
	public InputField email;
	public Text statusText;
	public bool status;
	//public Button register;
	//public Button back;
	//public Button login;

	public void onClickRegister(){
		checkPassword ();
		if (status) {
			Debug.Log ("true");
			StartCoroutine (registerUser (UserName.text, password.text, email.text));
		}

	}

	private void checkPassword(){
		if (password.text == repeatPassword.text) {
			status = true;
		} else {
			statusText.text = "Mismached password";
			status = false;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator registerUser (string name, string password, string email){
      

        WWWForm form = new WWWForm ();
		form.AddField ("namePost", name);
		form.AddField ("passwordPost", password);
		form.AddField ("emailPost", email);
        
		WWW www = new WWW (registrationURL, form);
		yield return www;
		statusText.text = www.text + "\n You can now login";
		Debug.Log (www.text);

	}

	/*public void checkUserName (){
		WWWForm form = new WWWForm ();
		form.AddField ("namePost", name);
		form.AddField ("passwordPost",password);

		WWW www = new WWW (loginURL, form);
		yield return www;
		stateText.text = www.text;
		//Debug.Log (www.text);
		if(www.text == "Login success"){
			isLogedIn = true;
		}else{
			isLogedIn = false;
		}
	}*/

}
