using UnityEngine;
using System.Collections;

public class Defence : MonoBehaviour {
	
	public KeyCode mKey;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(mKey)) {
			// Do defence stuffs
			
		}
	}
}
