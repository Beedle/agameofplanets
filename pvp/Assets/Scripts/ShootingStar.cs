using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootingStar : MonoBehaviour {
	private static float MAX_SPEED = 3f;
	
	public Vector2 velocity = new Vector2(1f, 1f);
	
	[SerializeField]
	private AnimationCurve mFadeCurve;
	
	private Light mLight;
	private float mTimer;
	private float mBaseIntensity;
	
	void Start() {
		mLight = gameObject.GetComponent<Light>();
		mBaseIntensity = mLight.intensity;
		
		AssignSpeed();
		
		Vector3 position = new Vector3(0f, 0f, 100f);
		position.x = Random.Range(-200, 200);
		position.y = Random.Range(-140, 140);
		transform.position = position;
	}
	
	void AssignSpeed() {
		mTimer = Random.Range(0f, 2f);
		velocity.x = Random.Range(-MAX_SPEED, MAX_SPEED);
		velocity.y = Random.Range(-MAX_SPEED, MAX_SPEED);	
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
		mTimer += Time.deltaTime * Random.Range(0.3f, 0.5f);
	}
}
