using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class active_on_move_frame : MonoBehaviour {

	public List<GameObject> gameObjects;
	

	// Regularly enable/disable the gameObjects based on status of this frame
	void Update() {
		bool active = music_manager.is_valid_move_frame();
		foreach (GameObject GO in gameObjects) {
			GO.SetActive(active);
		}
	}
}
