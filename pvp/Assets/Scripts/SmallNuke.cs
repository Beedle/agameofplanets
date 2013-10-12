using UnityEngine;
using System.Collections;

public class SmallNuke : Rocket {
	
	
	
	void Awake() {
		mType = Rocket.Type.SMALL_NUKE;	
	}
	
	public override float EnergyCost(){
		return 10f;
	}
	
}
