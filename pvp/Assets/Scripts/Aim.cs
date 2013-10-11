using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {
	protected KeyCode mLeftKey;
	public KeyCode LeftKey {
		get { return mLeftKey; }
		set { mLeftKey = value; }
	}
	
	protected KeyCode mRightKey;
	public KeyCode RightKey {
		get { return mRightKey; }
		set { mRightKey = value; }
	}
	
	void Start () {
		
	}
	
	void Update () {
		if (Input.GetKey(mLeftKey)) {
			RotateAlongZ(-90f);
		} else if (Input.GetKey(mRightKey)) {
			RotateAlongZ(90f);
		}
	}
	
	
	private void RotateAlongZ(float degree) {
		Vector3 euler = transform.rotation.eulerAngles;
		euler.z += degree * Time.deltaTime;
		transform.rotation = Quaternion.Euler(euler);
	}
}
