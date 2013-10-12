
using UnityEngine;
using System.Collections;

public class Planet : Body {
	public enum PlayerSide {
		PLAYER_UNDEFINED,
		PLAYER_LEFT,
		PLAYER_RIGHT,
	}
	
	public Body mOrbitBody;
	public float mOrbitDistance;
	
	public PlayerSide mPlayerSide = PlayerSide.PLAYER_UNDEFINED;
	public Aim mAim;
	
	public float mOrbitXFactor = 1f;
	public float mOrbitYFactor = 1f;
	
	public float mTimer = 0f;
	
	protected int mHealth = 100;
	public int health {
		get { return mHealth; }
		set { mHealth = value; }
	}
	
	
	protected override void Start() {
		base.Start();
		
	
		
		if (mPlayerSide == PlayerSide.PLAYER_LEFT) {
			mAim.LeftKey = KeyCode.Q;
			mAim.RightKey = KeyCode.W;
			mAim.FireKey = KeyCode.E;
		} else if (mPlayerSide == PlayerSide.PLAYER_RIGHT) {
			mAim.LeftKey = KeyCode.O;
			mAim.RightKey = KeyCode.P;
			mAim.FireKey = KeyCode.I;
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
		mHealth -= 10;
		
		if (mHealth <= 0) {
			if (mPlayerSide == PlayerSide.PLAYER_LEFT) {
				Debug.Log("Left got PÅWND!");	
			}
			
			else {
				Debug.Log("Right got PÅWND!");	
			}
			
			this.gameObject.SetActive(false);
		}
	}
}
