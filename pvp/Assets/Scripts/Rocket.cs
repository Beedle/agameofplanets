using UnityEngine;
using System.Collections;

public class Rocket : Body {
	
	protected override void Start() {
		Mass = 1f;	
	}
	
	public void SetInitialVelocity(Vector2 velocity) {
		mVelocity = velocity;	
	}
}
