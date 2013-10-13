using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Camera : MonoBehaviour {
	// Everything is allowed in crunch and prototype.
	private static Camera _singleton;
	public static Camera Singleton {
		get { return _singleton; }	
	}
	
	private float mTravelTime;
	private float mMagnitude;
	private float mTimer;
	
	private bool mDoShake;

	void Awake() {
		_singleton = this;	
	}
	
	void Start () {
		
	}
	
	void Update () {
		if (mDoShake) {
			mTimer += Time.deltaTime;
			
			Vector3 pos = transform.position;
			pos.x += (mTimer / mTravelTime) * (mMagnitude - pos.x);
			transform.position = pos;
			
			if (mTimer >= mTravelTime) {
				mTravelTime = mTravelTime * 0.8f;
				mMagnitude = -mMagnitude * 0.8f;
				mTimer = 0f;
				
				if (mTravelTime < 0.04f) {
					mDoShake = false;
					transform.position = new Vector3(0f, 0f, -50f);
				}
			}
		}
	}
	
	
	public void Shake(float magnitude) {
		mMagnitude = magnitude / 2f;
		mTravelTime = 0.15f;
		mTimer = 0f;
		mDoShake = true;
	}
}
