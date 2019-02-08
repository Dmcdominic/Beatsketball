﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {
	// AudioSources
	public AudioSource airhorn;
	public AudioSource buzzer;
	public AudioSource ball_bounce;

	public static SoundManager instance = null;

	public float lowPitchRange;
	public float highPitchRange;


	// Static instance setup and initialization
	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		SceneManager.activeSceneChanged += onSceneChanged;
	}

	private void onSceneChanged(Scene current, Scene next) {
		//if (next.buildIndex < 2 && !titleTrack.isPlaying) {
		//	titleTrack.Play();
		//	lvlTrack.Stop();
		//} else if (next.buildIndex >= 2 && !lvlTrack.isPlaying) {
		//	titleTrack.Stop();
		//	lvlTrack.Play();
		//}
	}


	public void playAirhorn() {
		airhorn.pitch = Random.Range(lowPitchRange, highPitchRange);
		airhorn.Play();
	}

	public void playBuzzer() {
		buzzer.pitch = Random.Range(lowPitchRange, highPitchRange);
		buzzer.Play();
	}

	public void playBallBounce() {
		//ball_bounce.pitch = Random.Range(lowPitchRange, highPitchRange);
		ball_bounce.Play();
	}

}