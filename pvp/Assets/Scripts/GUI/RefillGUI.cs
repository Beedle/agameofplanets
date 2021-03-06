﻿using UnityEngine;
using System.Collections;

public class RefillGUI : PlayerGUIBehaviour {
	
	public Texture mBackTexture;
	public Texture mFillTexture;
	public Planet.PlayerSide mPlayerSide;
	
	private float mEnergy;
	
	
	void Start () {
		
	}
	
	void Update () {
		mEnergy += Time.deltaTime * 10f;
		if (mEnergy > 100f) mEnergy = 100f;
		if (mEnergy < 0.0f) mEnergy = 0f;
	}
	
	public void OnTimingKey(float timeOffset, int combo) {
		float score = 1f - timeOffset;
		if (score < 0f) return;
		
		mEnergy += score * combo;
	}
	
	public bool FireRocket(Rocket rocket) {
		float cost = rocket.EnergyCost();
		
		if (mEnergy >= cost) {
			mEnergy -= cost;
			return true;
		}
		
		return false;
	}
	
	void OnGUI() {
		SetGUIMatrix();
		DrawActionLabels();
		
		// Draw the background texture
		Rect rect = new Rect();
		rect.x = ((mPlayerSide == Planet.PlayerSide.PLAYER_LEFT) ? 25f : 1024f-25f-200f );
		rect.y = 768-180-25;
		rect.width = 200f;
		rect.height = 60f;
		GUI.DrawTexture(rect, mBackTexture);
		
		// Draw the fill
		rect.x += 6f;
		rect.y += 6f;
		rect.height -= 12f;
		rect.width = (188f) * (mEnergy / 100f);
		GUI.DrawTexture(rect, mFillTexture);
	}
}
