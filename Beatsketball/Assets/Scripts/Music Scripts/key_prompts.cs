using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class key_prompts {

	public static List<key_prompt>[] open_prompts = new List<key_prompt>[] { new List<key_prompt>(), new List<key_prompt>() };

	public static readonly List<string> keys = new List<string> { "B", "A", "Y", "X" };
	//public static readonly string shoot_key = "X";
	public static readonly string shoot_key = "L&R";
	public static readonly string LBumper_key = "LBumper";
	public static readonly string RBumper_key = "RBumper";


	public static void add_prompt(key_prompt prompt) {
		open_prompts[prompt.player].Add(prompt);
	}

	public static bool check_all_prompts(int player_index) {
		// Testing - todo: remove
		if (Input.GetAxisRaw("LBumper_1") != 0f) {
			Debug.Log("Got Lbumper1 test axis!");
		}
		if (Input.GetAxisRaw("LBumper_2") != 0f) {
			Debug.Log("Got Lbumper2 test axis!");
		}
		if (Input.GetAxisRaw("RBumper_1") != 0f) {
			Debug.Log("Got Rbumper1 test axis!");
		}
		if (Input.GetAxisRaw("RBumper_2") != 0f) {
			Debug.Log("Got Rbumper2 test axis!");
		}

		string player_string = (player_index + 1).ToString();
		bool passed = true;

		// Find all the keys that were pressed on this frame
		List<string> keys_down = new List<string>();
		foreach (string key in keys) {
			if (Input.GetButtonDown(key + "_" + player_string) && !music_manager.just_cleared_buffer) {
				keys_down.Add(key);
			}
		}

		// Check if they're trying to shoot
		bool LBumper_last = Input.GetButtonDown(LBumper_key + "_" + player_string) && Input.GetButton(RBumper_key + "_" + player_string);
		bool RBumper_last = Input.GetButton(LBumper_key + "_" + player_string) && Input.GetButtonDown(RBumper_key + "_" + player_string);
		if (LBumper_last || RBumper_last) {
			keys_down.Add(shoot_key);
		}

		// Set all successful prompts to be removed
		List<key_prompt> to_remove = new List<key_prompt>();
		foreach (key_prompt prompt in open_prompts[player_index]) {
			if (music_manager.is_valid_for_time(prompt.time_at_beat)) {
				if (keys_down.Contains(prompt.key)) {
					Debug.Log("Pressed the correct key!: " + prompt.key);
					Debug.Log("Current displacement from beat: " + music_manager.get_big_beat_displacement());
					to_remove.Add(prompt);
					keys_down.Remove(prompt.key);
					music_manager.clear_visual_prompt(prompt);
					// Check if its a shooting prompt
					if (prompt.shooting) {
						music_manager.Music_Manager.shoot_the_ball(player_index);
						// todo - should we just return here?
						return true;
					}
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
	public static string get_random_key(int total_keys) {
		return keys[Random.Range(0, Mathf.Clamp(total_keys, 1, keys.Count))];
	}

}

// A struct to store a key prompt that a certain player must successfully hit,
// within beat_range seconds of time_at_beat.
public struct key_prompt {
	public int player;
	public string key;
	public float time_at_beat;
	public bool shooting;
	public key_prompt(int _player, string _key, float _time_at_beat, bool _shooting) {
		player = _player;
		key = _key;
		time_at_beat = _time_at_beat;
		shooting = _shooting;
	}
}

// A struct which stores result == false if a prompt was missed or wrong button pressed,
// and result == true otherwise. If a prompt was hit on the beat, includes the prompt as well.
public struct prompt_result {
	public bool result;
	public key_prompt prompt;
	public prompt_result(bool _result, key_prompt _prompt) {
		result = _result;
		prompt = _prompt;
	}
}
