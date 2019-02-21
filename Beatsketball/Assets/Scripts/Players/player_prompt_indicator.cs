using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class player_prompt_indicator : MonoBehaviour {

	public int player_index;

	public SpriteRenderer button;
	public TextMeshPro textMeshPro;

	public Sprite button_sprite;
	public Sprite bumpers_sprite;


	// Update is called once per frame
	void Update() {
		if (!music_manager.playing || music_manager.shooting == shooting_state.shot) {
			set_prompt_visible(false);
			return;
		}

		key_prompt next_prompt = key_prompts.get_next_prompt(player_index);
		if (next_prompt.player == -1) {
			set_prompt_visible(false);
			return;
		}
		bool is_this_beat = music_manager.is_within_this_big_beat(next_prompt.time_at_beat);

		if (!is_this_beat) {
			set_prompt_visible(false);
			return;
		}

		set_prompt_visible(true);
		button.color = prompt_spawner.get_prompt_color(next_prompt.key);

		if (next_prompt.shooting) {
			button.sprite = bumpers_sprite;
			textMeshPro.text = "";
		} else {
			button.sprite = button_sprite;
			textMeshPro.text = next_prompt.key;
		}
	}

	// Hide or show the visual key prompt
	private void set_prompt_visible(bool visible) {
		button.gameObject.SetActive(visible);
	}
}
