using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		audio.loop = false;
	}
	public AudioClip mSound;
	
	public void Play() {
		//audio.clip = mSound;
		audio.PlayOneShot(mSound);
	}
}
