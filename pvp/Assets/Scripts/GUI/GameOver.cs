﻿using UnityEngine;
using System.Collections;

public class GameOver : PlayerGUIBehaviour {
	string mWinner = "NO";
	
	void Start () {
	
	}
	
	public void SetWinner(Planet.PlayerSide side) {
		if (side == Planet.PlayerSide.PLAYER_LEFT) {
			mWinner = "left";	
		} else if (side == Planet.PlayerSide.PLAYER_RIGHT) {
			mWinner = "right";	
		} else {
			mWinner = "none";	
		}
		
		AddActionLabel(mWinner + " hand side player won!", 
				new Vector2(512f, 50f),
				new Vector2(512f, 50f), float.MaxValue, 50, Color.white);
		
		AddActionLabel("The other guy suck",
				new Vector2(512f, 400f),
				new Vector2(512f, 400f), float.MaxValue, 30, Color.white);
	}
	
	void OnGUI() {
		SetGUIMatrix();
		
		DrawActionLabels();
		GUI.Box(new Rect(0f, 0f, 1024f, 768f), "");
		
		if (GUI.Button(new Rect(512-60, 768-90, 120, 60), "REMATCH")) {
			Application.LoadLevel(0);	
		}
	}
}
