using UnityEngine;
using System.Collections;

public class Sun : Body {
	protected override void Start() {
		base.Start();
		

	}
	
	protected override void Update() {
		base.Update();
		
		Vector3 euler = transform.rotation.eulerAngles;
		euler.y += 120f * Time.deltaTime;
		transform.rotation = Quaternion.Euler(euler);
	}
	
	protected override void UpdateVelocity() {
		// DO NOTHING
	}
}
