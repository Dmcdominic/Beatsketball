using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prompt_target : MonoBehaviour {
	public event_object player_failed;


	// Init
	private void Awake() {
		player_failed.addListener(on_fail);
	}

	// Show a red X, cuz you failed
	private void on_fail() {
		GetComponent<Animator>().SetTrigger("failed");
	}
}
