﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Body : MonoBehaviour {
	// All Body-objects add themselves to this list
	public static List<Body> sBodies = new List<Body>();
	
	// This flag must be toggled when the game is over
	public static bool gameOver = false;
	
	// The velocity of the body.
	protected Vector2 mVelocity = new Vector2(0f, 0f);
	public Vector2 Velocity {
		get { return mVelocity; }
	}
	
	// The mass of the body
	public float mMass;
	
	
	protected virtual void Start () {
		sBodies.Add (this);	
	}
	
	protected virtual void Update () {
		if (!gameOver) {
			UpdateVelocity();
			AddVelocityToPosition();
			rotate();
		}
	}
	
	void OnDestroy() {
		sBodies.Remove(this);	
	}
	
	protected void rotate() {
		Vector3 euler = transform.rotation.eulerAngles;
		euler.z += 0f * Time.deltaTime;
		transform.rotation = Quaternion.Euler(euler);
	}
	
	protected virtual void OnTriggerEnter(Collider collider) {
		Rocket rocket = collider.GetComponent<Rocket>();
		if (rocket != null && rocket.HasCompletedInitialTouch()) {
			OnRocketCollide(rocket);
			rocket.DestroyRocket();
		}
	}
	
	protected virtual void OnTriggerExit(Collider collider) {
		Rocket rocket = collider.GetComponent<Rocket>();
		if (rocket != null) {
			rocket.InitialTouchCompleted();
			//rocket.SetParent(this);
		}
	}
	
	
	/* Callback for registering whenever a rocket
	 * hits the body. By default, nothing happens.
	 */
	protected virtual void OnRocketCollide(Rocket rocket) {
		// Dooby dooby doo	
	}
	
	
	/* Updates the velocity according to all other bodies.
	 * 
	 * Note: The only "output" of the method is to re-assign
	 * the value of 'mVelocity'. mVelocity should not be affected
	 * by the delta-time.
	 */
	protected virtual void UpdateVelocity() {
		Vector2 acceleration = GetAccelerationOfBody(transform.position);
		mVelocity += acceleration * Time.deltaTime;
	}
	
	public Vector2 GetAccelerationOfBody(Vector3 myPosition, float myMass=-1f) {
		Vector2 acceleration = new Vector2();
		
		if (myMass < 0f) myMass = mMass;
		
		foreach (Body tempBody in sBodies) {
			if (tempBody != this && tempBody.gameObject.activeSelf) {
				
				Vector3 direction = tempBody.transform.position - myPosition;
				direction.Normalize();
				
				float distance = Vector3.Distance(myPosition, tempBody.transform.position);
				
				float pull = myMass * tempBody.mMass / distance * distance;
				
				acceleration += new Vector2(direction.x * pull, direction.y * pull);
			}
		}
		
		return acceleration;
	}
	
	protected void AddVelocityToPosition() {
		Vector3 nPos = transform.position;
		nPos.x += mVelocity.x * Time.deltaTime + 0f;
		nPos.y += mVelocity.y * Time.deltaTime + 0f;
		nPos.z += 0f;
		transform.position = nPos;
	}
	
	protected bool CheckPosition() {
		if (Vector3.Distance(transform.position, new Vector3(0,0,0)) > 600) {
			return false;
		}
		else return true;
	}
		
}

