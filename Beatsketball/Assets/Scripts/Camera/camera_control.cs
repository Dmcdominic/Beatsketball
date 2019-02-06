using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_control : MonoBehaviour {

	private float initial_size;

	// Init
	void Awake() {
		initial_size = Camera.main.orthographicSize;
	}

	// Update is called once per frame
	void Update() {
		// Zoom based on Time.timeScale
		Camera.main.orthographicSize = initial_size / Time.timeScale;
	}
}
