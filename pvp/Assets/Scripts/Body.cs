using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour {
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
	
	void Start () {
		
	}
	
	void Update () {
	
	}
}

