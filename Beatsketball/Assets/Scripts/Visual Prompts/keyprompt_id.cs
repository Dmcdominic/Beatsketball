using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyprompt_id : MonoBehaviour {
	public keyPrompt_event_object prompt_success;

	public key_prompt key_Prompt;


	// Init
	private void Awake() {
		prompt_success.e.AddListener(on_prompt_success);
	}

	// Checks if it was this prompt, and plays the green checkmark anim if so
	private void on_prompt_success(key_prompt prompt) {
		if (prompt.Equals(key_Prompt)) {
			GetComponent<Animator>().enabled = true;
			GetComponent<Animator>().SetTrigger("success");
		}
	}

	// Destroys this gameObject.
	// Used for an animation event after keyprompt success.
	public void destroy_gameObject() {
		Destroy(gameObject);
	}
}
