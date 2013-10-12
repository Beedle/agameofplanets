using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {
	public Rocket pfRocket;
	
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
	
	protected KeyCode mFireKey;
	public KeyCode FireKey {
		get { return mFireKey; }
		set { mFireKey = value; }
	}
	
	private float mRotZ = 90f;
	
	void Start () {
		
	}
	
	void Update () {
		if (Input.GetKey(mLeftKey)) {
			RotateAlongZ(90f);
		} else if (Input.GetKey(mRightKey)) {
			RotateAlongZ(-90f);
		} else if (Input.GetKeyDown(mFireKey)) {
			FireRocket();	
		}
	}
	
	public void FireRocketAI( Vector3 pos) {
		
		Vector3 position = pos;
		Quaternion rotation = Quaternion.Euler (0f, mRotZ, 0f);
		
		Rocket rocket = Instantiate(pfRocket, position, rotation) as Rocket;
		
		Body body = transform.parent.gameObject.GetComponent<Planet>();
		Vector2 velocity = body.Velocity;
		
		velocity.x = Mathf.Cos(Mathf.Deg2Rad * mRotZ) * 100f;
		velocity.y = Mathf.Sin(Mathf.Deg2Rad * mRotZ) * 100f;
		rocket.SetInitialVelocity(velocity);
	}
	
	private void FireRocket() {
		Vector3 position = transform.position;
		Quaternion rotation = Quaternion.Euler (0f, mRotZ, 0f);
		
		Rocket rocket = Instantiate(pfRocket, position, rotation) as Rocket;
		
		Body body = transform.parent.gameObject.GetComponent<Planet>();
		Vector2 velocity = body.Velocity;
		
		velocity.x = Mathf.Cos(Mathf.Deg2Rad * mRotZ) * 100f;
		velocity.y = Mathf.Sin(Mathf.Deg2Rad * mRotZ) * 100f;
		rocket.SetInitialVelocity(velocity);
	}
	
	private void RotateAlongZ(float degree) {
		Vector3 euler = transform.rotation.eulerAngles;
		euler.z += degree * Time.deltaTime;
		transform.rotation = Quaternion.Euler(euler);
		
		mRotZ = euler.z + 90f;
	}
	
}
