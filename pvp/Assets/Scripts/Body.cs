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
				
				//m1*m2/dist*dist
				float massProduct = tempBody.Mass * mMass;
				Vector3 distance = tempBody.transform.position - transform.position;
				acceleration.x += massProduct / (distance.x * distance.x);
				acceleration.y += massProduct / (distance.y * distance.y);
			}
		}
		
		mVelocity += acceleration;
	}
	
	protected void AddVelocityToPosition() {
		Vector3 nPos = transform.position;
		nPos.x += mVelocity.x * Time.deltaTime;
		nPos.y += mVelocity.y * Time.deltaTime;
		transform.position = nPos;
	}
}

