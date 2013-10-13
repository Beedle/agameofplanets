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
	
	private static Vector3 sIntroStart = new Vector3(0f, 240f, -50f);
	private static Vector3 sIntroDest = new Vector3(0f, 0f, -50f);
	
	[SerializeField]
	private AnimationCurve mCurve;
	private static float INTRO_TIME = 2.5f;
	private float mIntroTimer = 0f;

	void Awake() {
		_singleton = this;	
	}
	
	void Start () {
		transform.position = new Vector3(0f, 240f, -50f);
	}
	
	void Update () {
		if (mIntroTimer < INTRO_TIME) {
			float f = mIntroTimer / INTRO_TIME;
			float p = mCurve.Evaluate(f);
			
			transform.position = sIntroDest + p * sIntroStart;
			mIntroTimer += Time.deltaTime;
			
		} else if (mDoShake) {
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
