using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_set : MonoBehaviour {

	public int player_set_index;
	public GameObject offense;


	// Init with all children inactive
	private void Start() {
		set_all_children(false);
	}

	// Check the current offensive team every frame,
	// in order to update which player set is active
	void Update() {
		set_all_children(music_manager.offense_p == player_set_index);
	}

	// Set all children active/inactive
	private void set_all_children(bool active) {
		foreach (Transform child in transform) {
			child.gameObject.SetActive(active);
		}
	}
}
