using UnityEngine;
using System.Collections;

public class Planet : Body {
	public Body mOrbitBody;
	public float mOrbitDistance;
	
	public float mOrbitXFactor = 1f;
	public float mOrbitYFactor = 1f;
	
	public float mTimer = 0f;
	
	protected override void UpdateVelocity() {
		mTimer += Time.deltaTime;
		
		Vector3 position = mOrbitBody.transform.position;
		
		// Calculate the distance to the parent body
		position.x += Mathf.Cos(mTimer * 0.5f) * mOrbitXFactor * mOrbitDistance;
		position.y += Mathf.Sin(mTimer * 0.5f) * mOrbitYFactor * mOrbitDistance;
		
		transform.position = position;
	}
}
