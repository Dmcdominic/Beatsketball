using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyprompt_id : MonoBehaviour {
	public keyPrompt_event_object prompt_success;

	public Sprite good_sprite;
	public Sprite great_sprite;
	public Sprite perfect_sprite;

	public key_prompt key_Prompt;

	private Image image;

	private static readonly float good_adjust = 0.05f;
	private static readonly float great_adjust = 0.1f;
	private static readonly float perfect_adjust = 0.3f;


	// Init
	private void Awake() {
		image = GetComponent<Image>();
		prompt_success.e.AddListener(on_prompt_success);
	}

	// Checks if it was this prompt, and plays the green checkmark anim if so
	private void on_prompt_success(key_prompt prompt, press_accuracy accuracy) {
		if (prompt.Equals(key_Prompt)) {
			GetComponent<Animator>().enabled = true;
			GetComponent<Animator>().SetTrigger("success");

			RectTransform RT = gameObject.GetComponent<RectTransform>();
			Vector2 anchor_min = RT.anchorMin;
			Vector2 anchor_max = RT.anchorMax;

			switch (accuracy) {
				case press_accuracy.good:
					RT.anchorMin = new Vector2(anchor_min.x - good_adjust, anchor_min.y);
					RT.anchorMax = new Vector2(anchor_max.x + good_adjust, anchor_max.y);
					image.sprite = good_sprite;
					break;
				case press_accuracy.great:
					RT.anchorMin = new Vector2(anchor_min.x - great_adjust, anchor_min.y);
					RT.anchorMax = new Vector2(anchor_max.x + great_adjust, anchor_max.y);
					image.sprite = great_sprite;
					break;
				case press_accuracy.perfect:
					RT.anchorMin = new Vector2(anchor_min.x - perfect_adjust, anchor_min.y);
					RT.anchorMax = new Vector2(anchor_max.x + perfect_adjust, anchor_max.y);
					image.sprite = perfect_sprite;
					break;
			}
		}
	}

	// Destroys this gameObject.
	// Used for an animation event after keyprompt success.
	public void destroy_gameObject() {
		Destroy(gameObject);
	}
}
