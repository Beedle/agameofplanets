using UnityEngine;
using System.Collections;


/* Note that this class is not a MonoBehaviour derived class.
 * It's instantiated and managed by PlayerGUIBehaviour-instances.
 */
public class ActionLabel {
	private string mText;
	private Rect mRect;
	
	private Vector2 mStartPos;
	private Vector2 mEndPos;
	
	private float mTotalTime;
	private float mTimer;
	
	private GUIStyle mStyle;
	
	public ActionLabel(string text, Vector2 start, Vector2 end, float time) {
		mRect = new Rect();
		mRect.width = 1024f;
		mRect.height = 768f;
		
		mText = text;
		mStartPos = start;
		mEndPos = end;
		mTotalTime = time;
		mTimer = 0f;
		
		mStyle = new GUIStyle();
	}
	
	public void SetGUIStyle(GUIStyle style) {
		mStyle = style;
	}
	
	private bool Update () {
		mTimer += Time.deltaTime;
		if (mTimer >= mTotalTime) {
			return false;	
		}
		
		mRect.x = mStartPos.x + (mEndPos.x * (mTimer / mTotalTime));
		mRect.y = mStartPos.y + (mEndPos.y * (mTimer / mTotalTime));
		
		return true;
	}
	
	public bool DrawGUI() {
		if (!Update()) {
			return false;
		}
		
		GUI.Label(mRect, mText, mStyle);
		return true;
	}
}
