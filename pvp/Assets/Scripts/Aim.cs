using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {
	public Rocket pfRocket;
	public DefRocket pfDefRocket;
	
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
	
	protected KeyCode mDefFireKey;
	public KeyCode DefFireKey {
		get { return mDefFireKey; }
		set { mDefFireKey = value; }
	}
	
	private float mRotZ = 90f;
	private RefillGUI mRefillGUI;
	
	void Start () {
		mRefillGUI = transform.parent.GetComponent<RefillGUI>();
	}
	
	void Update () {
		if (Input.GetKey(mLeftKey)) {
			RotateAlongZ(90f);
		} else if (Input.GetKey(mRightKey)) {
			RotateAlongZ(-90f);
		} else if (Input.GetKeyDown(mFireKey)) {
			FireRocket();	
		} else if (Input.GetKeyDown(mDefFireKey)) {
			FireDefRocket();
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
		
		velocity.x += Mathf.Cos(Mathf.Deg2Rad * mRotZ) * 100f;
		velocity.y += Mathf.Sin(Mathf.Deg2Rad * mRotZ) * 100f;
		rocket.SetInitialVelocity(velocity);
	}
	
	private void FireDefRocket() {
		Vector3 position = transform.position;
		Quaternion rotation = Quaternion.Euler (0f, mRotZ, 0f);
		
		DefRocket rocket = Instantiate(pfDefRocket, position, rotation) as DefRocket;
		
		Body body = transform.parent.gameObject.GetComponent<Planet>();
		Vector2 velocity = body.Velocity;
		
		velocity.x += Mathf.Cos(Mathf.Deg2Rad * mRotZ) * 25f;
		velocity.y += Mathf.Sin(Mathf.Deg2Rad * mRotZ) * 25f;
		rocket.SetInitialVelocity(velocity);
	}
	
	private void RotateAlongZ(float degree) {
		Vector3 euler = transform.rotation.eulerAngles;
		euler.z += degree * Time.deltaTime;
		transform.rotation = Quaternion.Euler(euler);
		
		mRotZ = euler.z + 90f;
	}
	
}
