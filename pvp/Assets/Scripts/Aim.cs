using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 euler = transform.rotation.eulerAngles;
		euler.z += 1f;
		transform.rotation = Quaternion.Euler(euler);
	}
}
