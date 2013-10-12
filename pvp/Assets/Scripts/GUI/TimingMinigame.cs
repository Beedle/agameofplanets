using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimingMinigame : PlayerGUIBehaviour {
	private class CharItem {
		public float timer;
		public char  key;
	}
	
	private static char[] sLeftKeys = new char[] {
		'a', 's', 'd', 'f',
		'z', 'x', 'c', 'v',
	};
	
	private static char[] sRightKeys = new char[] {
		'g', 'h', 'j', 'k', 'l',
		'b', 'n', 'm'
	};
	
	
	// The side of the player. Determines HUD position and key selection.
	public Planet.PlayerSide mPlayerSide;
	
	// The GUI Content
	public Texture mGuiTexture;
	
	// The Refill GUI
	private RefillGUI mRefillGUI;
	
	// The expected sequence of characters
	private List<CharItem> mSequence = new List<CharItem>();
	private float mAddTimer;
	private int mCombo;
	
	
	void Start() {
		AddCharItem();
		AddCharItem();
		mSequence[0].timer -= 1f;
		
		mRefillGUI = gameObject.GetComponent<RefillGUI>();
	}
	
	void Update () {
		CheckKeyInput();
		
		UpdateCharItems();
		
		// Add new items
		mAddTimer += Time.deltaTime;
		if (mAddTimer >= 1f) {
			mAddTimer = 0f;
			AddCharItem();
		}
	}
	
	void CheckKeyInput() {
		if (mSequence.Count != 0) {
			CharItem next = mSequence[0];
			char key = next.key;
			
			if (Input.GetKeyDown(""+key)) {
				float accuracy = Mathf.Abs(next.timer);
				if (accuracy < 0.3f) {
					mCombo++;
				} else {
					mCombo = 0;
				}
				
				mRefillGUI.OnTimingKey(accuracy, mCombo);
				mSequence.RemoveAt(0);
			}
		}
	}
	
	void UpdateCharItems() {
		for (int i=0; i<mSequence.Count; i++) {
			CharItem item = mSequence[i];
			item.timer -= Time.deltaTime;
			
			if (item.timer < -0.2f) {
				mSequence.RemoveAt(i--);
				mCombo = 0;
			}
		}
	}
	
	void AddCharItem() {
		char[] keySet = null;
		if (mPlayerSide == Planet.PlayerSide.PLAYER_LEFT) {
			keySet = sLeftKeys;	
		} else if (mPlayerSide == Planet.PlayerSide.PLAYER_RIGHT) {
			keySet = sRightKeys;	
		} else return;
		
		int idx = Random.Range(0, keySet.Length);
		
		CharItem item = new CharItem();
		item.key = keySet[idx];
		item.timer = 5f;
		mSequence.Add(item);
	}
	
	void OnGUI() {
		SetGUIMatrix();
		DrawActionLabels();
		
		// Draw the background
		Rect rect = new Rect();
		rect.x = ((mPlayerSide == Planet.PlayerSide.PLAYER_LEFT) ? 25f : 1024f-25f-200f );
		rect.y = 768-120-25;
		rect.width = 200f;
		rect.height = 120f;
		GUI.Box(rect, mGuiTexture);
		
		// Draw the letters
		GUIStyle style = new GUIStyle();
		style.fontSize = 20;
		style.alignment = TextAnchor.MiddleCenter;
		
		for (int i=0; i<mSequence.Count; i++) {
			CharItem item = mSequence[i];	
			
			Rect txtRect = new Rect(rect.x, rect.y, 20f, rect.height);
			
			float min = rect.x + rect.width - 15;
			float max = -165f;
			txtRect.x = min + (1f - (item.timer / 5f)) * max;
			txtRect.x -= txtRect.width/2f;
			
			GUI.Box(txtRect, ""+item.key, style);
		}
	}
}
