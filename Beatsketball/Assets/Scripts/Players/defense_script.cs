using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defense_script : MonoBehaviour {
	// Public fields
	public List<GameObject> defenders;
	public GameObject offense;

	// Private vars
	private Dictionary<int, int> lanes;
	//private int counter = 0;
	private int current_defender;
	private int prev_vert_input_sign = 0;
	private bool moved_this_frame = false;


	// Init
	void Start() {
		// Set the initial defensive player index
		current_defender = 0;

		// Initialize the lanes dictionary
		lanes = new Dictionary<int, int>();
		for (int player_index = 0; player_index < 3; player_index++) {
			lanes.Add(player_index, 1);
		}
	}

	// Update is called once per frame
	void Update() {
		if (!music_manager.playing || music_manager.facing_off) {
			return;
		}

		// Take in the input and determine if you should change lanes
		float p2_vertical_input = Input.GetAxis("Vertical_2");


		if (p2_vertical_input > 0 && prev_vert_input_sign <= 0 && lanes[current_defender] > 0) {
			if (music_manager.is_valid_big_frame() && !moved_this_frame) {
				offense.transform.position += offense_script.moveDirection;
				prev_vert_input_sign = 1;
				lanes[current_defender] -= 1;
				moved_this_frame = true;
			} else {
				on_bad_lane_input();
			}
		}

		if (p2_vertical_input < 0 && prev_vert_input_sign >= 0 && lanes[current_defender] < 2) {
			if (music_manager.is_valid_big_frame() && !moved_this_frame) {
				offense.transform.position -= offense_script.moveDirection;
				prev_vert_input_sign = -1;
				lanes[current_defender] += 1;
				moved_this_frame = true;
			} else {
				on_bad_lane_input();
			}
		}

		// switch defensive player
		if (offense.transform.position.x > defenders[current_defender].transform.position.x) {
			if (current_defender < 2) {
				current_defender++;
			} else {
				// todo - transition to shooting
			}
		}
	}

	// Called when the player tries to change lanes with bad timing
	private void on_bad_lane_input() {
		print("bad defender lane input");
		// todo - handle bad timing for lane input, for defenders
	}
}
