using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Camera : MonoBehaviour {
	// Everything is allowed in crunch and prototype.
	private static Camera _singleton;
	public static Camera Singleton {
		get { return _singleton; }	
	}
	
	// Shakessss
	private struct ShakeDef {
		public List<Vector2> positions;
		public List<float> timers;
		public List<float> totalTime;
	}
	
	// ((((shaekkekekekekke))))
	private List<ShakeDef> mShakes = new List<ShakeDef>();
	
	
	void Awake() {
		_singleton = this;	
	}
	
	void Start () {
			
	}
	
	void Update () {
		for (int i=0; i<mShakes.Count; i++) {
			float factor = Time.deltaTime / mShakes[i].totalTime[0];
			
			Vector3 position = transform.position;
			position.x += factor * mShakes[i].positions[0].x;
			position.y += factor * mShakes[i].positions[0].y;
			
			mShakes[i].timers[0] += Time.deltaTime;
			if (mShakes[i].timers[0] >= mShakes[i].totalTime[0]) {
				mShakes[i].timers.RemoveAt(0);
				mShakes[i].totalTime.RemoveAt(0);
				mShakes[i].positions.RemoveAt(0);
				
				if (mShakes[i].positions.Count == 0) {
					mShakes.RemoveAt(i--);	
				}
			}
		}
	}
	
	
	public void Shake(float time, float magnitude) {
		ShakeDef def = new ShakeDef();
		def.positions = new List<Vector2>();
		def.timers = new List<float>();
		def.totalTime = new List<float>();
		
		for (int i=0; i<9; i++) {
			float angle = Random.Range(0f, 6.24f);
			Vector2 dir = new Vector2();
			dir.x = Mathf.Cos(angle) * magnitude * Random.Range(0.5f, 1.5f);
			dir.y = Mathf.Sin(angle) * magnitude * Random.Range(0.5f, 1.5f);
			
			def.positions.Add(dir);
			def.timers.Add(0f);
			def.totalTime.Add(Random.Range(0.6f, 1.4f) * time/10f);
		}
		
		def.positions.Add(new Vector2(0f, 0f));
		def.timers.Add(0f);
		def.totalTime.Add(time/10f);
	}
}
