using UnityEngine;
using System.Collections;

public class Planet : Body {
	public Body mOrbitBody;
	public float mOrbitDistance;
	public float mTimer = 0f;
	
	private static float sOrbitTime = 20f;
	
	protected override void UpdateVelocity() {
		mTimer += Time.deltaTime;
		
		mVelocity.x = Mathf.Cos (mTimer / sOrbitTime) * 2.5f * mOrbitDistance;
		mVelocity.y = Mathf.Sin (mTimer / sOrbitTime) * mOrbitDistance;
	}
}
