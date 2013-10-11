using UnityEngine;
using System.Collections;

public class Planet : Body {
	public Body mOrbitBody;
	
	private static float sOrbitTime = 20f;
	
	private float mTimer;
	
	protected override void UpdateVelocity() {
		mTimer += Time.deltaTime;
		
		mVelocity.x = Mathf.Cos (mTimer / sOrbitTime) * 2.5f;
		mVelocity.y = Mathf.Sin (mTimer / sOrbitTime);
	}
}
