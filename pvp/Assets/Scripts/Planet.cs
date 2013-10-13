
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
	

	float WaitTime;

	// The dying planet is responsible for triggering
	// the game over screen.
	public GameOver pfGameOverScreen;
	
	protected static List<Planet> PlayerPlanets = new List<Planet>();
	public List<GameObject> moons;
	
	public Body mOrbitBody;
	public float mOrbitDistance;
	
	public PlayerSide mPlayerSide = PlayerSide.PLAYER_UNDEFINED;
	public Aim mAim;
	public AudioClip mSoundBoom;
	
	public float speed = 1;
	
	public float mOrbitXFactor = 1f;
	public float mOrbitYFactor = 1f;
	
	public float xMod = 0;
	public float yMod = 0;
	
	public float mTimer = 0f;
	
	protected float mHealth = 100;
	public float health {
		get { return mHealth; }
		set { mHealth = value; }
	}
		
	protected override void Start() {
		base.Start();
		
		WaitTime = Time.time;
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
				
		}
		
		moons = new List<GameObject>();
		for (int i=0; i<transform.childCount; i++) {
			GameObject child = transform.GetChild(i).gameObject;
			if (child.name == "Moon") {
				moons.Add(child);	
			}
		}
	}
	
		
	protected override void UpdateVelocity() {
		mTimer += Time.deltaTime;
		
		Vector3 position = mOrbitBody.transform.position;
		
		// Calculate the distance to the parent body
		position.x += Mathf.Cos(mTimer * 0.5f * speed) * mOrbitXFactor * mOrbitDistance + xMod;
		position.y += Mathf.Sin(mTimer * 0.5f * speed) * mOrbitYFactor * mOrbitDistance + yMod;
		
		transform.position = position;
	}
	
	protected override void OnRocketCollide(Rocket rocket) {
		float damage = rocket.Damage();
		mHealth -= damage;

		audio.PlayOneShot(mSoundBoom);

		if (mPlayerSide == PlayerSide.PLANET_AI) {
			mHealth = 100;	
		}

		if (mPlayerSide == PlayerSide.PLAYER_LEFT || mPlayerSide == PlayerSide.PLAYER_RIGHT) {
			for (int i=4; i>=(int)mHealth/20 && mHealth > 0 && mPlayerSide != PlayerSide.PLANET_AI; i--) {
				moons[i].SetActive(false);	
			}
		}
		
		
		if (mHealth <= 0f) {
			Body.gameOver = true;
			GameOver go = Instantiate(pfGameOverScreen) as GameOver;
			
			// Flag the winner
			PlayerSide winner = ((mPlayerSide == PlayerSide.PLAYER_LEFT) 
									? PlayerSide.PLAYER_RIGHT 
									: PlayerSide.PLAYER_LEFT);
			go.SetWinner(winner);
		}
		
		if (mPlayerSide == PlayerSide.PLANET_AI) {

						
		
			Planet target = PlayerPlanets[FindClosestPlayer()];
		
			transform.LookAt(target.transform.position);
			mAim.FireRocket(100f, transform.position);
	
		
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
