using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	private AudioSource[] sounds;

	public void MuteButtonPressed(){
		sounds = FindObjectsOfType<AudioSource> ();
		foreach(AudioSource sound in sounds){
			if (sound.mute) {
				sound.mute = false;
			} else {
				sound.mute = true;
			}
		}
	}


}
