using UnityEngine;
using System.Collections;

public class Planet : Body {
	public Body mOrbitBody;
	public float mOrbitDistance;
	public float mTimer = 0f;
	
	private static float sOrbitTime = 1f;
	
	protected override void UpdateVelocity() {
		mTimer += Time.deltaTime;
		
		Vector3 position = mOrbitBody.transform.position;
		position.x += Mathf.Cos(mTimer / sOrbitTime) * 2.5f * mOrbitDistance;
		position.y += Mathf.Sin(mTimer / sOrbitTime) * mOrbitDistance;
		
		transform.position = position;
	}
}
