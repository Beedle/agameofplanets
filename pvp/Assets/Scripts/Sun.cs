using UnityEngine;
using System.Collections;

public class Sun : Body {
	protected override void Start() {
		base.Start();
		
		Mass = 100f;	
	}
}
