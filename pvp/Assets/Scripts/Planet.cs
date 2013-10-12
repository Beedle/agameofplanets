
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planet : Body {
	public enum PlayerSide {
		PLAYER_UNDEFINED,
		PLAYER_LEFT,
		PLAYER_RIGHT,
		PLANET_AI,
	}
	
	protected static List<Planet> PlayerPlanets = new List<Planet>();
	public GameObject[] moons;
	
	public Body mOrbitBody;
	public float mOrbitDistance;
	
	public PlayerSide mPlayerSide = PlayerSide.PLAYER_UNDEFINED;
	public Aim mAim;
	
	public float mOrbitXFactor = 1f;
	public float mOrbitYFactor = 1f;
	
	public float mTimer = 0f;
	
	protected float mHealth = 100;
	public float health {
		get { return mHealth; }
		set { mHealth = value; }
	}
	
	
	protected override void Start() {
		base.Start();
		
		if (mPlayerSide == PlayerSide.PLAYER_LEFT) {
			PlayerPlanets.Add(this);
			mAim.LeftKey = KeyCode.Q;
			mAim.RightKey = KeyCode.W;
			mAim.DefFireKey = KeyCode.E;
			mAim.FireSmallKey = KeyCode.R;
			mAim.FireLargeKey = KeyCode.T;
		} else if (mPlayerSide == PlayerSide.PLAYER_RIGHT) {
			PlayerPlanets.Add(this);
			mAim.LeftKey = KeyCode.O;
			mAim.RightKey = KeyCode.P;
			mAim.DefFireKey = KeyCode.I;
			mAim.FireSmallKey = KeyCode.U;
			mAim.FireLargeKey = KeyCode.Y;
		} else if (mPlayerSide == PlayerSide.PLANET_AI) {	
			renderer.material.color = Color.green;	
		}
	}
	
	protected override void UpdateVelocity() {
		mTimer += Time.deltaTime;
		
		Vector3 position = mOrbitBody.transform.position;
		
		// Calculate the distance to the parent body
		position.x += Mathf.Cos(mTimer * 0.5f) * mOrbitXFactor * mOrbitDistance;
		position.y += Mathf.Sin(mTimer * 0.5f) * mOrbitYFactor * mOrbitDistance;
		
		transform.position = position;
	}
	
	protected override void OnRocketCollide(Rocket rocket) {
		
		
		float damage = rocket.Damage();
		mHealth -= damage;
		
		for (int i=4; i>=(int)mHealth/20; i--) {
			moons[i].SetActive(false);	
		}
		
		if (mHealth <= 0f) {
			Body.gameOver = true;	
		}
		
		if (mPlayerSide == PlayerSide.PLANET_AI) {
			rocket.DestroyRocket();
			
			Debug.Log("AI ");	
			renderer.material.color = Color.red;
			
			Planet target = PlayerPlanets[FindClosestPlayer()];
			transform.LookAt(target.transform.position);
			mAim.FireRocket(mAim.pfSmallRocket, 100f);
		}
	}
	
	protected int FindClosestPlayer() {
		
		float DistRightPlayer;
		float DistLeftPlater;
		int numRightPlayer;
		int numLeftPlayer;
		
		if( PlayerPlanets[0].mPlayerSide == PlayerSide.PLAYER_RIGHT) {
			
			DistRightPlayer = Vector3.Distance(PlayerPlanets[0].transform.position, this.transform.position);
			numRightPlayer = 0;
			DistLeftPlater = Vector3.Distance(PlayerPlanets[1].transform.position, this.transform.position);
			numLeftPlayer = 1;
		}
		else {
			
			DistRightPlayer = Vector3.Distance(PlayerPlanets[1].transform.position, this.transform.position);
			numRightPlayer = 1;
			DistLeftPlater = Vector3.Distance(PlayerPlanets[0].transform.position, this.transform.position);
			numLeftPlayer = 0;
		}
		
		if (DistLeftPlater < DistRightPlayer) {
			
			return numLeftPlayer;
		}
		else {
			
			return numRightPlayer;	
		}
	}
	
	
}
