using UnityEngine;
using System.Collections;

public class ParticleEffect : MonoBehaviour {
	public float mTimer = 10f;
	
	void Start () {
		
	}
	
	void Update () {
		mTimer -= Time.deltaTime;
		if (mTimer < 0f) {
			Destroy(gameObject);
		}
	}
}
