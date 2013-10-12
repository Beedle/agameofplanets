using UnityEngine;
using System.Collections;

public class DefRocket : Rocket {
	
	
	void Awake() {
		mType = Rocket.Type.DEFENSIVE;	
	}
	
	// Use this for initialization
	protected override void UpdateVelocity() {
		Vector2 acceleration = new Vector2();
		
		
		for (int i = 0; i < sBodies.Count; i++) {
		//foreach (Body tempBody in sBodies) {
			Body tempBody = sBodies[i];
			
			Rocket tempRocket = tempBody as Rocket;
			
			
			if (tempBody != this && tempBody.gameObject.activeSelf && tempRocket != null) {
				
				Vector3 direction = tempBody.transform.position - transform.position;
				direction.Normalize();
				
				float distance = Vector3.Distance(transform.position, tempBody.transform.position);
				
				float pull = 100 * mMass * tempBody.mMass / distance * distance;
				
				acceleration += new Vector2(direction.x * pull, direction.y * pull);
			}
		}
		
		mVelocity += acceleration * Time.deltaTime;
	}
	
	public override float EnergyCost() {
		return 10f;
	}
	
	public override float Damage() {
		return 2f;	
	}
}
