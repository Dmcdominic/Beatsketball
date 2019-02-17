using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class prompt_spawner : MonoBehaviour {

	public Rigidbody2D prompt_prefab;
	public GameObject prompt_target;
	public Sprite bumpers_sprite;
	public keyPrompt_event_object spawn_keyPrompt;


	// Init
	private void Awake() {
		spawn_keyPrompt.e.AddListener(spawn_visual_prompt);
	}

	// Spawn a prompt
	private void spawn_visual_prompt(key_prompt prompt) {
		Rigidbody2D new_prompt = Instantiate(prompt_prefab, transform);
		new_prompt.GetComponentInChildren<TextMeshProUGUI>().text = prompt.key;

		if (prompt.shooting) {
			new_prompt.GetComponent<Image>().sprite = bumpers_sprite;
			new_prompt.GetComponentInChildren<TextMeshProUGUI>().text = "";
		}

		new_prompt.GetComponent<Image>().color = get_prompt_color(prompt.key);
		new_prompt.GetComponent<keyprompt_id>().key_Prompt = prompt;

		new_prompt.transform.position = transform.position;
		new_prompt.velocity = (prompt_target.transform.position - new_prompt.transform.position) / (music_manager.Music_Manager.big_beat_interval * 2);
	}

	// Returns the corresponding color for each key prompt
	public static Color get_prompt_color(string prompt_key) {
		switch (prompt_key) {
			case "A":
				return Color.red;
			case "B":
				return Color.yellow;
			case "X":
				return Color.blue;
			case "Y":
				return Color.green;
		}
		// Default case - For shooting the basket, or other
		return Color.white;
	}
}
