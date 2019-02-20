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
			// todo - use accuracy to change sprite
			switch(accuracy) {
				case press_accuracy.good:
					image.sprite = good_sprite;
					break;
				case press_accuracy.great:
					image.sprite = great_sprite;
					break;
				case press_accuracy.perfect:
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
