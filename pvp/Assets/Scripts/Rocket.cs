using UnityEngine;
using System.Collections;

public abstract class Rocket : Body {
	public enum Type {
		DEFENSIVE,
		SMALL_NUKE,
		LARGE_NUKE,
	}
	
	protected Type mType;
	public Type RocketType {
		get { return mType; }
	}
	
	private bool mInitialTouchComplete = false;
	private GameObject mMyParent;
	
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
	
	
	// Abstract Rocket Methods
	public abstract float EnergyCost();
	
	
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
	
	public void SetParent(GameObject parent) {
		
		mMyParent = parent;	
	}
	

	public GameObject GetParent() {
		
		return mMyParent;
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
