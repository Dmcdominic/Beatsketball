using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music_manager : MonoBehaviour {

	// Public fields
	public bool disable_prompt_spawns = false;

	public track Track;
	public float beat_range; // The leeway, in seconds, to land something on the beat
	public float faceoff_timescale_increment;
	public float just_cleared_buffer_time;

	public event_object to_trigger_on_start_song;
	public event_object to_trigger_on_stop_song;
	public event_object to_trigger_on_beat;
	public event_object to_trigger_on_big_beat;
	public keyPrompt_event_object spawn_p1_keyPrompt;
	public keyPrompt_event_object spawn_p2_keyPrompt;
	public keyPrompt_event_object prompt_success;
	public event_object p1_failed;
	public event_object p2_failed;

	public float beat_interval { get; private set; }
	public float big_beat_interval { get; private set; }

	// Private vars
	private float prev_disp = 0;
	private float prev_big_disp = 0;

	// Public static vars
	public static AudioSource audioSource;

	public static bool playing = false;
	public static bool facing_off = false;
	public static bool just_cleared_buffer = false;

	public static int offense_p = 0;

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
		beat_interval = (60f / Track.bpm);
		big_beat_interval = beat_interval * 2f;
	}

	// Start the scene
	private void Start() {
		audioSource.clip = Track.song;
		start_offense(0);
	}

	// Start the song, and the gameplay
	private void start_offense(int new_offense_p) {
		offense_p = new_offense_p;
		delete_all_prompts();
		// todo - anything else needs to be set here?

		prev_disp = 0;
		prev_big_disp = 0;
		facing_off = false;
		playing = true;
		audioSource.Play();
		to_trigger_on_start_song.Invoke();
	}

	// Switches the possession to player with index new_offense_p.
	// This may be temporary, depending on how transitioning between possessions goes.
	private void switch_possession(int new_offense_p) {
		offense_p = new_offense_p;
		delete_all_prompts();
		// todo - anything else needs to be set here?
		prev_disp = 0;
		prev_big_disp = 0;
		facing_off = false;
		playing = true;
	}

	// Call this when the game should end. Stop the song, and the gameplay
	private void game_over() {
		delete_all_prompts();
		audioSource.Stop();
		to_trigger_on_stop_song.Invoke();
		facing_off = false;
		playing = false;
		// Todo - anything else here? Show the score and buttons for: "Rematch" and "Main Menu"?
	}

	// Call this to initiate a faceoff
	public void start_faceoff() {
		//delete_all_prompts();
		facing_off = true;
		SoundManager.instance.playAirhorn();
	}

	// Call this when someone wins the faceoff
	private void end_faceoff(int winning_player_index) {
		facing_off = false;
		delete_all_prompts();
		if (winning_player_index != offense_p) {
			// todo - cheering here?
			switch_possession(winning_player_index);
		} else {
			// todo - cheering here?
			// todo - defender falls over or something here?
		}
	}

	// Update is called once per frame
	void Update() {
		if (!playing) {
			return;
		} else if (playing && !audioSource.isPlaying) {
			// todo - this means song has ended(?), so end the game
			game_over();
		}

		// Manually initiate a faceoff, or force a possession swap (editor only)
#if UNITY_EDITOR
		if (Input.GetButtonDown("Force_Faceoff")) {
			start_faceoff();
		}
		if (Input.GetButtonDown("Force_P1_Offence")) {
			switch_possession(0);
		} else if (Input.GetButtonDown("Force_P2_Offence")) {
			switch_possession(1);
		}
#endif

		// Update audioSource pitch based on current timeScale
		audioSource.pitch = Time.timeScale;

		// Check for triggering the beat
		float new_disp = get_beat_displacement();
		if (prev_disp < 0f && new_disp >= 0f) {
			on_beat();
		}
		prev_disp = new_disp;

		// Check for triggering the BIG beat
		float new_big_disp = get_big_beat_displacement();
		if (prev_big_disp < 0f && new_big_disp >= 0f) {
			on_big_beat();
		}
		prev_big_disp = new_big_disp;

		// Check all key prompt inputs
		bool p1_pass = key_prompts.check_all_prompts(0);
		bool p2_pass = key_prompts.check_all_prompts(1);

		if (!p1_pass) {
			p1_failed.Invoke();
			SoundManager.instance.playBuzzer();
		}
		if (!p2_pass) {
			p2_failed.Invoke();
			SoundManager.instance.playBuzzer();
		}

		if (!facing_off) {
			// Normal offensive key prompts
			if (offense_p == 0 && !p1_pass) {
				// todo - p1 loses possession here
				switch_possession(1);
			} else if (offense_p == 1 && !p2_pass) {
				// todo - p2 loses possession here
				switch_possession(0);
			}
		} else if (p1_pass ^ p2_pass) {
			// Faceoff end condition, assuming exactly 1 player failed
			if (!p1_pass) {
				end_faceoff(1);
			} else if (!p2_pass) {
				end_faceoff(0);
			}
		}
	}

	// If you are facing off, steadily increase the timescale
	private void FixedUpdate() {
		if (!playing) {
			return;
		}
		if (facing_off) {
			Time.timeScale += faceoff_timescale_increment * Time.fixedUnscaledDeltaTime;
		} else if (Time.timeScale > 1) {
			Time.timeScale = Mathf.Max(1, Time.timeScale - faceoff_timescale_increment * 10f * Time.fixedUnscaledDeltaTime);
		}
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

	// Returns, in seconds, how close the track is to the nearest BIG beat (this frame).
	public static float get_big_beat_displacement() {
		if (audioSource.time < Music_Manager.Track.start_time) {
			return audioSource.time - Music_Manager.Track.start_time;
		}

		float time_since = audioSource.time - Music_Manager.Track.start_time;
		float displacement = time_since % Music_Manager.big_beat_interval;
		float negative_disp = displacement - Music_Manager.big_beat_interval;

		float result = (Mathf.Abs(displacement) <= Mathf.Abs(negative_disp)) ? displacement : negative_disp;
		return result;
	}

	// Triggered on the frame when a beat occurs
	private void on_beat() {
		to_trigger_on_beat.Invoke();
	}

	// Triggered every other beat
	private void on_big_beat() {
		to_trigger_on_big_beat.Invoke();

		if (playing && !facing_off) {
			// If we are playing, but not facing off, spawn a standard dribble key_prompt for offense
			//string key = key_prompts.dribble_key;
			string key = key_prompts.get_random_key(4);
			make_prompt(new key_prompt(offense_p, key, Time.time + big_beat_interval*2));
		} else if (playing && facing_off) {
			// If we are facing off, spawn a random key_prompt for both players
			string key = key_prompts.get_random_key(2);
			make_prompt(new key_prompt(0, key, Time.time + big_beat_interval * 2));
			make_prompt(new key_prompt(1, key, Time.time + big_beat_interval * 2));
		}
	}

	// Spawns flying keyprompts, and adds them to the key_prompts list
	private void make_prompt(key_prompt prompt) {
		if (disable_prompt_spawns) {
			return;
		}

		if (prompt.player == 0) {
			spawn_p1_keyPrompt.Invoke(prompt);
		} else {
			spawn_p2_keyPrompt.Invoke(prompt);
		}
		key_prompts.add_prompt(prompt);
	}

	// Clears a visual prompt that was successful, and plays checkmark
	public static void clear_visual_prompt(key_prompt prompt) {
		Music_Manager.prompt_success.Invoke(prompt);
		SoundManager.instance.playBallBounce();
	}

	// Deletes all flying keyprompts and clears them from the key_prompts lists
	private void delete_all_prompts() {
		// Destroy all the visual keyprompts
		GameObject[] all_prompts = GameObject.FindGameObjectsWithTag("prompt");
		foreach (GameObject obj in all_prompts) {
			Destroy(obj);
		}
		// Clear all the prompts in the key_prompts lists
		key_prompts.clear_all_prompts();
		// Set the just cleared buffer for a short time
		StartCoroutine(start_just_cleared_buffer());
	}

	// Returns true iff we are within beat_range seconds of a beat
	public static bool is_valid_frame() {
		return Mathf.Abs(get_beat_displacement()) <= Music_Manager.beat_range;
	}

	// Returns true iff we are within beat_range seconds of a big beat
	public static bool is_valid_big_frame() {
		return Mathf.Abs(get_big_beat_displacement()) <= Music_Manager.beat_range;
	}

	// Returns true iff we are within beat_range seconds of a given time
	public static bool is_valid_for_time(float time) {
		return Mathf.Abs(Time.time - time) <= Music_Manager.beat_range;
	}

	// Returns true iff we have missed the valid window for a certain time
	public static bool missed_window(float time) {
		return time + Music_Manager.beat_range < Time.time;
	}

	// Returns the amount of time left before the song ends
	public static float song_time_remaining() {
		return audioSource.clip.length - audioSource.time;
	}

	// Sets the just_cleared_buffer to true, then resets it after a short duration
	private IEnumerator start_just_cleared_buffer() {
		just_cleared_buffer = true;
		yield return new WaitForSeconds(just_cleared_buffer_time);
		just_cleared_buffer = false;
	}
}

public enum shooting_state { not, waiting, on_its_way, shot };
