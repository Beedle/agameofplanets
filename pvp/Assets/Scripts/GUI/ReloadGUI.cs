using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReloadGUI : MonoBehaviour {
	private static char[] sLeftKeys = new char[] {
		 't',
		'a', 's', 'd', 'f',
		'z', 'x', 'c', 'v',
	};
	
	private static char[] sRightKeys = new char[] {
		'y',
		'g', 'h', 'j', 'k', 'l',
		'b', 'n', 'm'
	};
	
	// The side of the player. Determines HUD position and key selection.
	public Planet.PlayerSide mPlayerSide;
	
	// The expected sequence of characters
	private List<char> mSequence = new List<char>();
	
	// The GUI Content
	public Texture mGuiTexture;
	
	
	static bool IsKeyOnSide(char key, Planet.PlayerSide side) {
		char[] keySet = null;
		
		if (side == Planet.PlayerSide.PLAYER_LEFT) {
			keySet = sLeftKeys;
		} else if (side == Planet.PlayerSide.PLAYER_RIGHT) {
			keySet = sRightKeys;	
		} else return false;
		
		foreach (char ch in keySet) {
			if (ch == key) {
				return true;
			}
		}
		
		return false;
	}
	
	static List<char> GetRandomSequence(Planet.PlayerSide side) {
		char[] keySet = null;
		
		if (side == Planet.PlayerSide.PLAYER_LEFT) {
			keySet = sLeftKeys;
		} else if (side == Planet.PlayerSide.PLAYER_RIGHT) {
			keySet = sRightKeys;
		} else return null;
		
		// Get 3 random and unique characters
		List<char> list = new List<char>();
		
		while (list.Count < 3) {
			int idx = Random.Range(0, (int)keySet.Length);
			
			bool unique = true;
			for (int j=0; j<list.Count; j++) {
				if (list[j] == keySet[idx]) {
					unique = false;
				}
			}
			
			if (unique) {
				list.Add(keySet[idx]);
			}
		}
		
		return list;
	}
	
	
	public bool CanFire() {
		return mSequence.Count == 0;
	}
	
	public void StartMinigame() {
		mSequence = GetRandomSequence(mPlayerSide);
	}
	
	
	void Start () {
		
	}
	
	void Update () {
		if (mSequence.Count != 0) {
			char key = mSequence[0];
			if (Input.GetKeyDown(""+key)) {
				mSequence.RemoveAt(0);	
			}
		}
	}
	
	void OnGUI() {
		if (mSequence.Count == 0) {
			return;
		}
		
		// Prepare the rectangle for displaying the box
		Rect rect = new Rect();
		rect.x = Screen.width * ( (mPlayerSide == Planet.PlayerSide.PLAYER_LEFT) ? 0.05f : 0.8f );
		rect.y = Screen.height * 0.8f;
		rect.width = Screen.width * 0.15f;
		rect.height = Screen.height * 0.15f;
		
		char key = mSequence[0];
		
		GUIStyle style = new GUIStyle();
		style.fontSize = 25;
		style.alignment = TextAnchor.MiddleCenter;
		
		GUI.Box(rect, mGuiTexture);
		GUI.Box(rect, "" + key, style);
	}
}
