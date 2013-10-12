using UnityEngine;
using System.Collections;

public class Rocket : Body {
	private bool mInitialTouchComplete = false;
	
	protected override void Start() {
		base.Start();
		mMass = 1f;
	}
	
	protected override void Update() {
		base.Update();
		
		Vector3 rotation = new Vector3();
		rotation.z = Mathf.Atan2(mVelocity.y, mVelocity.x) * Mathf.Rad2Deg - 90f;
		transform.rotation = Quaternion.Euler(rotation);
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
	
	public void DestroyRocket() {
		// Remove particles from gameObject so they can slowly face
		// by themselves.
		ParticleEffect part = gameObject.GetComponentInChildren<ParticleEffect>();
		if (part != null) {
			part.transform.parent = null;	
			part.mTimer = 10f;
		}
		
		Destroy(gameObject);
	}
}
