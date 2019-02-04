using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music_manager : MonoBehaviour {

	// Public fields
	public track Track;
	public float beat_range; // The leeway, in seconds, to land something on the beat
	public int beats_per_playable_beat;

	public event_object to_trigger_on_start_song;
	public event_object to_trigger_on_stop_song;
	public event_object to_trigger_on_beat;
	public event_object to_trigger_on_big_beat;
	public bool_var playing;

	// Private vars
	private float beat_interval;
	private float prev_disp = 0;
	private bool prev_big_beat = false;

	public static AudioSource audioSource;


	// Singleton setup
	public static music_manager Music_Manager;


	// Init
	private void Awake() {
		if (Music_Manager != null && Music_Manager != this) {
			Destroy(gameObject);
			return;
		}

		Music_Manager = this;
		DontDestroyOnLoad(gameObject);
		audioSource = GetComponentInChildren<AudioSource>();
		beat_interval = (60f / Track.bpm) * (float)beats_per_playable_beat;
	}

	// Start the scene
	private void Start() {
		audioSource.clip = Track.song;
		start_song();
	}

	// Start the song, and the gameplay
	private void start_song() {
		prev_disp = 0;
		audioSource.Play();
		to_trigger_on_start_song.Invoke();
		playing.val = true;
	}

	// Stop the song, and the gameplay
	private void stop_song() {
		audioSource.Stop();
		to_trigger_on_stop_song.Invoke();
		playing.val = false;
	}

	// Update is called once per frame
	void Update() {
		if (!playing.val) {
			return;
		}

		// Update audioSource pitch based on current timeScale
		audioSource.pitch = Time.timeScale;

		// Check for triggering the beat
		float new_disp = get_beat_displacement();
		if (prev_disp < 0f && new_disp >= 0f) {
			on_beat();
		}
		prev_disp = new_disp;
	}

	// Returns, in seconds, how close the track is to the nearest beat (this frame).
	public static float get_beat_displacement() {
		if (audioSource.time < Music_Manager.Track.start_time) {
			return audioSource.time - Music_Manager.Track.start_time;
		}

		float time_since = audioSource.time - Music_Manager.Track.start_time;
		float displacement = time_since % Music_Manager.beat_interval;
		float negative_disp = displacement - Music_Manager.beat_interval;

		float result = (Mathf.Abs(displacement) <= Mathf.Abs(negative_disp)) ? displacement : negative_disp;
		return result;
	}

	// Triggered on the frame when a beat occurs
	private void on_beat() {
		to_trigger_on_beat.Invoke();
		if (!prev_big_beat) {
			on_big_beat();
		}
		prev_big_beat = !prev_big_beat;
	}

	// Triggered every other beat
	private void on_big_beat() {
		to_trigger_on_big_beat.Invoke();
	}

	// Returns true iff we are within
	public static bool is_valid_frame() {
		return Mathf.Abs(get_beat_displacement()) <= Music_Manager.beat_range;
	}
}
