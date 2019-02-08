using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class active_only_for_faceoff : MonoBehaviour {
	private TextMeshProUGUI textMeshProUGUI;

	private void Awake() {
		textMeshProUGUI = GetComponent<TextMeshProUGUI>();
	}

	void Update() {
		textMeshProUGUI.enabled = music_manager.facing_off;
	}
}
