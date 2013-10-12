using UnityEngine;
using System.Collections;

public class LargeNuke : Rocket {
	
	void Awake() {
		mType = Rocket.Type.LARGE_NUKE;	
	}
	
	public override float EnergyCost(){
		return 50f;
	}
	
	public override float Damage() {
		return 20f;	
	}
}
