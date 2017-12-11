using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour {

	private UserData theUserData;
	private string scoreTableType;
	public Text[] scoreTexts;
	//private string scoreDataURL = "http://localhost/Endless_Runner/scoreData.php";
    private string scoreDataURL = "http://193.219.91.103:3089/scoreData.php";
    public Text menuLabel;

	// Use this for initialization
	void Start () {
		theUserData = FindObjectOfType<UserData> ();
		if (FindObjectOfType<UserData> ()) {
			OnClickPersonal ();
		} else {
			scoreTexts [0].text = "You need to login";
		}
	}
	

	public void OnClickPersonal(){
		scoreTableType = "Personal";
		menuLabel.text = "Personal";
		if (FindObjectOfType<UserData> () && theUserData.userID != 0) {
            for (int i = 0; i < scoreTexts.Length; i++){
                scoreTexts[i].text = "";
            }
            StartCoroutine (GetScoreData (1, theUserData.userID));
		} else {
			scoreTexts [0].text = "You need to login";
			for (int i = 1; i < scoreTexts.Length; i++) {
				scoreTexts [i].text = "";
			}
		}

	}

	public void OnClickGlobal(){
		scoreTableType = "Global";
		menuLabel.text = scoreTableType;
		StartCoroutine (GetScoreData (0, 0));
	}

	IEnumerator GetScoreData(int type, int userId){
		WWWForm form = new WWWForm ();
		form.AddField ("typePost", type);
		form.AddField ("userIdPost",userId);
		WWW www = new WWW (scoreDataURL, form);
		yield return www;
		Debug.Log (www.text);
		ParseData (www.text);
	}

	public void ParseData(string data){
		string[] scoreLines = data.Split (';');
		for (int i = 0; i < scoreTexts.Length; i++) {
			scoreTexts [i].text = scoreLines [i];
		}
	}
}
