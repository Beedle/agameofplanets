using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootingStar : MonoBehaviour {
	public Vector2 velocity = new Vector2(1f, 1f);
	
	[SerializeField]
	private AnimationCurve mFadeCurve;
	
	private Light mLight;
	private float mTimer;
	private float mBaseIntensity;
	
	void Start() {
		mLight = gameObject.GetComponent<Light>();
		mBaseIntensity = mLight.intensity;
		
		mTimer = Random.Range(0f, 2f);
	}
	
	void Update () {
		Vector3 pos = transform.position;
		
		pos.x += velocity.x * Time.deltaTime;
		pos.y += velocity.y * Time.deltaTime;
			
		if (pos.x < -300f) {
			pos.x = 290f;
		} else if (pos.x > 300f) {
			pos.x = -290f;	
		}
		
		if (pos.y < -150f) {
			pos.y = 140f;	
		} else if (pos.y > 150f) {
			pos.y = -140f;
		}
		
		transform.position = pos;
			
		mLight.intensity = mFadeCurve.Evaluate(mTimer) * mBaseIntensity;
		mTimer += Time.deltaTime * Random.Range(0.8f, 1.2f);
	}
}
