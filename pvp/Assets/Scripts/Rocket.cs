using UnityEngine;
using System.Collections;

public class Rocket : Body {
	private bool mInitialTouchComplete = false;
	
	protected override void Start() {
		Mass = 1f;	
	}
	
	public void SetInitialVelocity(Vector2 velocity) {
		mVelocity = velocity;	
	}
	
	
	protected override void OnTriggerEnter(Collider c) {
		// DO NOTHING YO
	}
	
	protected override void OnTriggerExit(Collider ccc) {
		// DO NOTHING YO
	}
	
	
	public bool HasCompletedInitialTouch() {
		return mInitialTouchComplete;	
	}
	
	public void InitialTouchCompleted() {
		mInitialTouchComplete = true;	
	}
}
