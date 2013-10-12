using UnityEngine;
using System.Collections;

public class RefillGUI : PlayerGUIBehaviour {
	
	public Texture mGuiTexture;
	public Planet.PlayerSide mPlayerSide;
	
	
	void Start () {
		
	}
	
	void Update () {
			
	}
	
	void OnGUI() {
		SetGUIMatrix();
		
		Rect rect = new Rect();
		rect.x = ((mPlayerSide == Planet.PlayerSide.PLAYER_LEFT) ? 25f : 1024f-25f-200f );
		rect.y = 768-180-25;
		rect.width = 200f;
		rect.height = 60f;
		
		GUI.Box(rect, mGuiTexture);
	}
}
