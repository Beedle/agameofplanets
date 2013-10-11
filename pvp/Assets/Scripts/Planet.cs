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
		Vector2 offset = new Vector2();
		offset.x += Mathf.Cos(mTimer) * mOrbitXFactor * mOrbitDistance;
		offset.y += Mathf.Sin(mTimer) * mOrbitYFactor * mOrbitDistance;
		
		position.x += offset.x;
		position.y += offset.y;
		
		transform.position = position;
	}
}
