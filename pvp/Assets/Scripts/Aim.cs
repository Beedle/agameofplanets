using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {
	public Rocket pfSmallRocket;
	public Rocket pfLargeRocket;
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
	
	protected KeyCode mFireSmallKey;
	public KeyCode FireSmallKey {
		get { return mFireSmallKey; }
		set { mFireSmallKey = value; }
	}
	
	protected KeyCode mFireLargeKey;
	public KeyCode FireLargeKey {
		get { return mFireLargeKey; }
		set { mFireLargeKey = value; }
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
		} else if (Input.GetKeyDown(mFireSmallKey)) {
			FireRocket(pfSmallRocket, 100f);	
		} else if (Input.GetKeyDown(mDefFireKey)) {
			FireRocket(pfDefRocket, 25f);
		} else if (Input.GetKeyDown(mFireLargeKey)) {
			FireRocket(pfLargeRocket, 100f);	
		}
	}
	
	public void FireRocket(Rocket prefab, float speed) {
		if (!mRefillGUI.FireRocket(prefab)) {
			return;
		} 
		
		Vector3 position = transform.position;
		Quaternion rotation = Quaternion.Euler (0f, mRotZ, 0f);
		
		Rocket rocket = Instantiate(prefab, position, rotation) as Rocket;
		
		Body body = transform.parent.gameObject.GetComponent<Planet>();
		Vector2 velocity = body.Velocity;
		
		velocity.x += Mathf.Cos(Mathf.Deg2Rad * mRotZ) * speed;
		velocity.y += Mathf.Sin(Mathf.Deg2Rad * mRotZ) * speed;
		rocket.SetInitialVelocity(velocity);
	}
	
	private void RotateAlongZ(float degree) {
		Vector3 euler = transform.rotation.eulerAngles;
		euler.z += degree * Time.deltaTime;
		transform.rotation = Quaternion.Euler(euler);
		
		mRotZ = euler.z + 90f;
	}
	
}
