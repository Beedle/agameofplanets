using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGUIBehaviour : MonoBehaviour {
	private List<ActionLabel> mActionLabels = new List<ActionLabel>();
	
	protected void SetGUIMatrix() {
		float nativeWidth = 1024f;
		float nativeHeight = 768f;
		
		float rx = Screen.width / nativeWidth;
		float ry = Screen.height / nativeHeight;
		
		GUI.matrix = Matrix4x4.TRS(new Vector3(0f,0f,0f), Quaternion.identity, new Vector3(rx, ry, 1f));
	}
	
	
	protected void DrawActionLabels() {
		for (int i=0; i<mActionLabels.Count; i++) {
			if (!mActionLabels[i].DrawGUI()) {
				mActionLabels.RemoveAt(i--);
			}
		}
	}
	
	protected ActionLabel AddActionLabel(string text, Vector2 start, Vector2 end, 
										 float time, int fontSize, Color color) {
		ActionLabel label = new ActionLabel(text, start, end, time);
		
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = fontSize;
		style.normal.textColor = color;
		label.SetGUIStyle(style);
			
		mActionLabels.Add(label);
			
		return label;
	}
}
