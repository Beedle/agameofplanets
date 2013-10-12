using UnityEngine;
using System.Collections;

public class Rocket : Body {
	private bool mInitialTouchComplete = false;
	private GameObject mMyParent;
	
	protected override void Start() {
		Mass = 1f;	
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
	
	public void SetParent(GameObject parent) {
		
		mMyParent = parent;	
	}
	
	public GameObject GetParent() {
		
		return mMyParent;	
	}
}
