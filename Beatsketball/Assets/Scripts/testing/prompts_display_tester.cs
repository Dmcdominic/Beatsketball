using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class prompts_display_tester : MonoBehaviour {

	public int player_index;

	private TextMeshProUGUI textMesh;
	

	// Init
	void Awake() {
		textMesh = GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void LateUpdate() {
		string full_prompt_text = "";
		foreach (key_prompt prompt in key_prompts.open_prompts[player_index]) {
			full_prompt_text += prompt.key + "\n";
		}
		textMesh.text = full_prompt_text;
		
		if (music_manager.is_valid_big_frame()) {
			textMesh.fontStyle = FontStyles.Italic;
		} else {
			textMesh.fontStyle = FontStyles.Normal;
		}
	}
}
