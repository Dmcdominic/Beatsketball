﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defense_script : MonoBehaviour {
	// Public fields
	public int player_set_index;
	public GameObject offense;
	public List<GameObject> defenders;

	// Private vars
	private Dictionary<int, int> lanes;
	//private int counter = 0;
	private int current_defender;
	private int prev_vert_input_sign = 0;
	private bool moved_this_beat = false;


	// Init
	void Start() {
		// Initialize the lanes dictionary
		lanes = new Dictionary<int, int>();
		for (int player_index = 0; player_index < 3; player_index++) {
			lanes.Add(player_index, 1);
		}
	}
	
	// Initialization that needs to be done whenever a drive starts
	private void OnEnable() {
		// Set the initial defensive player index
		current_defender = 0;
	}

	// Update is called once per frame
	void Update() {
		if (!music_manager.playing || music_manager.facing_off) {
			return;
		}

		// Take in the input and determine if you should change lanes
		int defender_index = player_set_index == 0 ? 1 : 0;
		string input_axis = "Vertical_" + (defender_index + 1).ToString();
		float defense_vertical_input = Input.GetAxisRaw(input_axis);

		if (defense_vertical_input > 0 && prev_vert_input_sign <= 0 && lanes[current_defender] > 0) {
			if (music_manager.is_valid_big_frame() && !moved_this_beat) {
				defenders[current_defender].transform.position += offense_script.moveDirection;
				prev_vert_input_sign = 1;
				lanes[current_defender] -= 1;
				moved_this_beat = true;
			} else {
				on_bad_lane_input();
			}
		}

		if (defense_vertical_input < 0 && prev_vert_input_sign >= 0 && lanes[current_defender] < 2) {
			if (music_manager.is_valid_big_frame() && !moved_this_beat) {
				defenders[current_defender].transform.position -= offense_script.moveDirection;
				prev_vert_input_sign = -1;
				lanes[current_defender] += 1;
				moved_this_beat = true;
			} else {
				on_bad_lane_input();
			}
		}

		// switch defensive player
		bool set0_switch = player_set_index == 0 && offense.transform.position.x > defenders[current_defender].transform.position.x;
		bool set1_switch = player_set_index == 1 && offense.transform.position.x < defenders[current_defender].transform.position.x;
		if (set0_switch || set1_switch) {
			if (current_defender < 2) {
				current_defender++;
			} else {
				// todo - transition to shooting
			}
		}

		// Update the input sign from this frame
		if (prev_vert_input_sign > 0 && defense_vertical_input <= 0) {
			prev_vert_input_sign = 0;
		} else if (prev_vert_input_sign < 0 && defense_vertical_input >= 0) {
			prev_vert_input_sign = 0;
		}

		// Reset moved_this_beat
		if (!music_manager.is_valid_big_frame()) {
			moved_this_beat = false;
		}
	}

	// Called when the player tries to change lanes with bad timing
	private void on_bad_lane_input() {
		print("bad defender lane input");
		// todo - handle bad timing for lane input, for defenders
	}
}
