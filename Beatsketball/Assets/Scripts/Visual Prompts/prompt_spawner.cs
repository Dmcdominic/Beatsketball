using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class prompt_spawner : MonoBehaviour {

	public Rigidbody2D prompt_prefab;
	public GameObject prompt_target;
	public keyPrompt_event_object spawn_keyPrompt;


	// Init
	private void Awake() {
		spawn_keyPrompt.e.AddListener(spawn_visual_prompt);
	}

	// Spawn a prompt
	private void spawn_visual_prompt(key_prompt prompt) {
		Rigidbody2D new_prompt = Instantiate(prompt_prefab, transform);
		new_prompt.GetComponentInChildren<TextMeshProUGUI>().text = prompt.key;

		Color new_color = new_prompt.GetComponent<Image>().color;
		switch (prompt.key) {
			case "A":
				new_color = Color.red;
				break;
			case "B":
				new_color = Color.yellow;
				break;
			case "X":
				new_color = Color.blue;
				break;
			case "Y":
				new_color = Color.green;
				break;
		}
		new_prompt.GetComponent<Image>().color = new_color;

		new_prompt.transform.position = transform.position;
		new_prompt.velocity = (prompt_target.transform.position - new_prompt.transform.position) / (music_manager.Music_Manager.big_beat_interval * 2);
	}
}
