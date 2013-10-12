using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Body : MonoBehaviour {
	// All Body-objects add themselves to this list
	protected static List<Body> sBodies = new List<Body>();
	
	// The velocity of the body.
	protected Vector2 mVelocity = new Vector2(0f, 0f);
	public Vector2 Velocity {
		get { return mVelocity; }
	}
	
	// The mass of the body
	protected float mMass;
	public float Mass {
		get { return mMass; }
		set { mMass = value; }
	}
	
	protected virtual void Start () {
		sBodies.Add (this);	
	}
	
	protected virtual void Update () {
		UpdateVelocity();
		AddVelocityToPosition();
	}
	
	protected virtual void OnTriggerEnter(Collider collider) {
		Rocket rocket = collider.GetComponent<Rocket>();
		if (rocket != null && rocket.HasCompletedInitialTouch()) {
			this.OnRocketCollide(rocket);
			Destroy(rocket.gameObject);
		}
	}
	
	protected virtual void OnTriggerExit(Collider collider) {
		Rocket rocket = collider.GetComponent<Rocket>();
		if (rocket != null) {
			rocket.InitialTouchCompleted();
			rocket.SetParent(this);
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
		Vector2 acceleration = new Vector2();
		
		foreach (Body tempBody in sBodies) {
			if (tempBody != this) {
				
				Vector3 direction = tempBody.transform.position - transform.position;
				direction.Normalize();
				
				float distance = Vector3.Distance(transform.position, tempBody.transform.position);
				
				float pull = mMass * tempBody.Mass / distance * distance;
				
				acceleration += new Vector2(direction.x * pull, direction.y * pull);
			}
		}
		
		mVelocity += acceleration * Time.deltaTime;
	}
	
	protected void AddVelocityToPosition() {
		Vector3 nPos = transform.position;
		nPos.x += mVelocity.x * Time.deltaTime + 0f;
		nPos.y += mVelocity.y * Time.deltaTime + 0f;
		nPos.z += 0f;
		transform.position = nPos;
	}
}

