using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Body : MonoBehaviour {
	// All Body-objects add themselves to this list
	private static List<Body> sBodies;
	
	// The velocity of the body.
	private Vector2 mVelocity;
	public Vector2 Velocity {
		get { return mVelocity; }
	}
	
	// The mass of the body
	private float mMass;
	public float Mass {
		get { return mMass; }
		set { mMass = value; }
	}
	
	void Awake() {
		if (sBodies == null) {
			sBodies = new List<Body>();	
		}
		
		sBodies.Add (this);
	}
	
	void Start () {
		
	}
	
	void Update () {
	
	}
	
	
	public virtual void UpdateVelocity() {
		
	}
}

