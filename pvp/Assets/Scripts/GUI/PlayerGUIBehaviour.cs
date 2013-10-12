using UnityEngine;
using System.Collections;

public class PlayerGUIBehaviour : MonoBehaviour {
	protected void SetGUIMatrix() {
		float nativeWidth = 1024f;
		float nativeHeight = 768f;
		
		float rx = Screen.width / nativeWidth;
		float ry = Screen.height / nativeHeight;
		
		GUI.matrix = Matrix4x4.TRS(new Vector3(0f,0f,0f), Quaternion.identity, new Vector3(rx, ry, 1f));
	}
}
