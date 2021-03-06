﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	public static bool isPaused = false;
	public GameObject pauseMenuUI;

	private float prev_timeScale = 1f;
	private bool pause_held = false;

	private gameManager GameManager;


	// Start is called before the first frame update
	void Start() {
		GameManager = FindObjectOfType<gameManager>();
		pauseMenuUI.SetActive(false);
	}

	// Update is called once per frame
	void Update() {
		if (GameManager.gameHasEnded) {
			return;
		}

		//if (Input.GetKeyDown(KeyCode.Escape)) {
		if (Input.GetAxisRaw("pause") > 0.1f && !pause_held) {
			if (isPaused) {
				Resume();
			} else {
				Pause();
			}
		}

		pause_held = Input.GetAxisRaw("pause") > 0.1f;
	}

	public void Resume() {
		pauseMenuUI.SetActive(false);
		music_manager.audioSource.UnPause();
		logical_resume();
	}

	private void logical_resume() {
		Time.timeScale = prev_timeScale;
		isPaused = false;
	}

	void Pause() {
		pauseMenuUI.SetActive(true);
		music_manager.audioSource.Pause();
		prev_timeScale = Time.timeScale;
		Time.timeScale = 0f;
		isPaused = true;
		EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
	}

	public void MainMenu() {
		logical_resume();
		SceneManager.LoadScene(1);
	}

	public void Quit() {
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE
		Application.Quit();
#endif
	}
}
