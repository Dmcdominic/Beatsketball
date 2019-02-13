﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offense_script : MonoBehaviour {
	public int player_set_index;
	public GameObject offense;

	private int lane = 1;
	private int prev_vert_input_sign = 0;
	private bool moved_this_beat = false;

	public static readonly float speed = 0.5f;
	public static readonly float lane_delta_height = 1.5f;
	public static readonly Vector3 moveDirection = new Vector3(0, lane_delta_height, 0);


	// Update is called once per frame
	void Update() {
		if (!music_manager.playing || music_manager.facing_off) {
			return;
		}

		// Move forward down the court
		Vector3 forward = new Vector3(speed, 0, 0);
		offense.transform.position += forward * Time.deltaTime * (player_set_index == 1 ? -1f : 1f);

		// Take in the input and determine if you should change lanes
		string input_axis = "Vertical_" + (player_set_index + 1).ToString();
		float offense_vertical_input = Input.GetAxisRaw(input_axis);

		if (offense_vertical_input > 0 && prev_vert_input_sign <= 0 && lane > 0) {
			if (music_manager.is_valid_big_frame() && !moved_this_beat) {
				offense.transform.position += moveDirection;
				prev_vert_input_sign = 1;
				lane -= 1;
				moved_this_beat = true;
			} else {
				on_bad_lane_input();
			}
		}

		if (offense_vertical_input < 0 && prev_vert_input_sign >= 0 && lane < 2) {
			if (music_manager.is_valid_big_frame() && !moved_this_beat) {
				offense.transform.position -= moveDirection;
				prev_vert_input_sign = -1;
				lane += 1;
				moved_this_beat = true;
			} else {
				on_bad_lane_input();
			}
		}

		// Update the input sign from this frame
		if (prev_vert_input_sign > 0 && offense_vertical_input <= 0) {
			prev_vert_input_sign = 0;
		} else if (prev_vert_input_sign < 0 && offense_vertical_input >= 0) {
			prev_vert_input_sign = 0;
		}

		// Reset moved_this_beat
		if (!music_manager.is_valid_big_frame()) {
			moved_this_beat = false;
		}
	}

	// Called when the player tries to change lanes with bad timing
	private void on_bad_lane_input() {
		print("bad offense lane input");
		// todo - handle bad timing for lane input
	}

	// Check for collisions with defenders
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Defense") {
			// Start the faceoff
			music_manager.Music_Manager.start_faceoff();
		}
	}
}