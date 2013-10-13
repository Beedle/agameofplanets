using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimingMinigame : PlayerGUIBehaviour {
	private const int MAX_CHARS = 5;
	private const float CHAR_TRAVEL_TIME = 10f;
	private bool OverTenCombo;
	public AudioClip Guitar;
	public AudioClip Combo;
	
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
	private Vector2 mLabelPosition = new Vector2();
	private float mAddTimer;
	private int mCombo;
	
	
	void Start() {
		AddCharItem();
		AddCharItem();
		mSequence[0].timer -= CHAR_TRAVEL_TIME / MAX_CHARS;
		
		mRefillGUI = gameObject.GetComponent<RefillGUI>();
		audio.loop = false;
	}
	
	void Update () {
		CheckKeyInput();
		
		UpdateCharItems();
		
		// Add new items
		mAddTimer += Time.deltaTime;
		if (mAddTimer >= CHAR_TRAVEL_TIME / MAX_CHARS) {
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
				if (accuracy < 0.8f) {
					mCombo++;
					AddComboLabel();
				} else {
					if(OverTenCombo) {
						audio.PlayOneShot(Combo);
						OverTenCombo = false;
					}
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
			
			if (item.timer < -0.6f) {
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
		item.timer = CHAR_TRAVEL_TIME;
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
		
		mLabelPosition = new Vector2(rect.x + rect.width/2f, rect.y-150f);	
		
		// Draw the letters
		GUIStyle style = new GUIStyle();
		style.fontSize = 20;
		style.alignment = TextAnchor.MiddleCenter;
		
		for (int i=0; i<mSequence.Count; i++) {
			CharItem item = mSequence[i];	
			
			Rect txtRect = new Rect(rect.x, rect.y, 20f, rect.height);
			
			float min = rect.x + rect.width - 15;
			float max = -165f;
			txtRect.x = min + (1f - (item.timer / CHAR_TRAVEL_TIME)) * max;
			txtRect.x -= txtRect.width/2f;
			
			GUI.Box(txtRect, ""+item.key, style);
		}
	}
	
	void AddComboLabel() {
		if( mCombo != 0 && mCombo % 10 == 0) {
			OverTenCombo = true;
			audio.PlayOneShot(Guitar);	
		}
		Vector2 end = new Vector2(mLabelPosition.x, mLabelPosition.y - 200f);
		AddActionLabel(mCombo+"Xcombo", mLabelPosition, end, 3f, 35, Color.green);
	}
}
