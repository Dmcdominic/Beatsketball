using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class key_prompts {

	public static List<key_prompt>[] open_prompts = new List<key_prompt>[] { new List<key_prompt>(), new List<key_prompt>() };

	public static readonly List<string> keys = new List<string> { "A", "B", "X", "Y" };
	public static readonly string dribble_key = "B";


	public static void add_prompt(key_prompt prompt) {
		open_prompts[prompt.player].Add(prompt);
	}

	public static bool check_all_prompts(int player_index) {
		string player_string = (player_index + 1).ToString();
		bool passed = true;

		// Find all the keys that were pressed on this frame
		List<string> keys_down = new List<string>();
		foreach (string key in keys) {
			if (Input.GetButtonDown(key + "_" + player_string)) {
				keys_down.Add(key);
			}
		}

		// Set all successful prompts to be removed
		List<key_prompt> to_remove = new List<key_prompt>();
		foreach (key_prompt prompt in open_prompts[player_index]) {
			if (music_manager.is_valid_for_time(prompt.time_at_beat)) {
				if (keys_down.Contains(prompt.key)) {
					// todo - You pressed the right key! Visual celebration!
					Debug.Log("Pressed the correct key!: " + prompt.key);
					Debug.Log("Current displacement from beat: " + music_manager.get_big_beat_displacement());
					to_remove.Add(prompt);
					keys_down.Remove(prompt.key);
				}
			}
		}
		
		// Remove any prompts that have been passed
		foreach (key_prompt passed_prompt in to_remove) {
			open_prompts[player_index].Remove(passed_prompt);
		}

		// Check if any prompts have missed their window
		foreach (key_prompt prompt in open_prompts[player_index]) {
			if (music_manager.missed_window(prompt.time_at_beat)) {
				// todo - You missed this key prompt!
				Debug.Log("Player: " + player_string + " missed a key: " + prompt.key);
				to_remove.Add(prompt);
				passed = false;
			}
		}

		// Remove prompts that were failed
		foreach (key_prompt failed_prompt in to_remove) {
			open_prompts[player_index].Remove(failed_prompt);
		}

		// Check if any keys that were pressed on this frame were not correct
		if (keys_down.Count > 0) {
			// todo - You pressed a key that was not prompted, or badly timed!
			Debug.Log("Player: " + player_string + " pressed an unprompted, or badly timed, key: " + keys_down[0]);
			passed = false;
		}

		return passed;
	}

	// Deletes all outstanding key prompts
	public static void clear_all_prompts() {
		open_prompts[0].Clear();
		open_prompts[1].Clear();
	}

	// Returns the string for a random key in the set
	public static string get_random_key() {
		return keys[Random.Range(0, keys.Count - 1)];
	}

}

// A struct to store a key prompt that a certain player must successfully hit,
// within beat_range seconds of time_at_beat.
public struct key_prompt {
	public int player;
	public string key;
	public float time_at_beat;
	public key_prompt(int _player, string _key, float _time_at_beat) {
		player = _player;
		key = _key;
		time_at_beat = _time_at_beat;
	}
}
