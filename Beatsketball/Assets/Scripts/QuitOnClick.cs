using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuitOnClick : MonoBehaviour {
	public void OnClick() {
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE
		Application.Quit();
#endif
	}

}
