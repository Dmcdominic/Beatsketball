using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
	public static bool isPaused = false;
	public GameObject pauseMenuUI;

	private float prev_timeScale = 1f;


	// Start is called before the first frame update
	void Start() {
		pauseMenuUI.SetActive(false);
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (isPaused) {
				Resume();
			} else {
				Pause();
			}
		}
	}

	void Resume() {
		pauseMenuUI.SetActive(false);
		Time.timeScale = prev_timeScale;
		isPaused = false;
	}

	void Pause() {
		pauseMenuUI.SetActive(true);
		prev_timeScale = Time.timeScale;
		Time.timeScale = 0f;
		isPaused = true;
	}
}
