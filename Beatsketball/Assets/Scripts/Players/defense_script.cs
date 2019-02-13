using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defense_script : MonoBehaviour {
	// Public fields
	public int player_set_index;
	public GameObject offense;
	public List<GameObject> defenders;

	// Private vars
	private Dictionary<int, int> lanes;

	private int prev_vert_input_sign = 0;
	private bool moved_this_beat = false;
	private Vector3 initial_scale;
	
	public static Dictionary<int, int> set_0_lanes;
	public static Dictionary<int, int> set_1_lanes;
	public static int current_defender;


	// Init
	void Start() {
		// Initialize the lanes dictionary
		if (player_set_index == 0) {
			set_0_lanes = new Dictionary<int, int> ();
			lanes = set_0_lanes;
		} else {
			set_1_lanes = new Dictionary<int, int>();
			lanes = set_1_lanes;
		}
		for (int player_index = 0; player_index < 3; player_index++) {
			lanes.Add(player_index, 1);
		}
		initial_scale = defenders[0].transform.localScale;
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
			if (music_manager.is_valid_move_frame() && !moved_this_beat) {
				lanes[current_defender] -= 1;
				defenders[current_defender].transform.position += offense_script.moveDirection;
				defenders[current_defender].transform.localScale = initial_scale * (0.9f + (0.1f * lanes[current_defender]));
				prev_vert_input_sign = 1;
				moved_this_beat = true;
			} else {
				on_bad_lane_input();
			}
		}

		if (defense_vertical_input < 0 && prev_vert_input_sign >= 0 && lanes[current_defender] < 2) {
			if (music_manager.is_valid_move_frame() && !moved_this_beat) {
				lanes[current_defender] += 1;
				defenders[current_defender].transform.position -= offense_script.moveDirection;
				defenders[current_defender].transform.localScale = initial_scale * (0.9f + (0.1f * lanes[current_defender]));
				prev_vert_input_sign = -1;
				moved_this_beat = true;
			} else {
				on_bad_lane_input();
			}
		}

		// switch defensive player
		bool set0_switch = player_set_index == 0 && offense.transform.position.x > defenders[current_defender].transform.position.x && music_manager.offense_p == 0;
		bool set1_switch = player_set_index == 1 && offense.transform.position.x < defenders[current_defender].transform.position.x && music_manager.offense_p == 1;
		if ((set0_switch || set1_switch) && music_manager.shooting == shooting_state.not) {
			if (current_defender < 2) {
				current_defender++;
				music_manager.button_complexity++;
			} else {
				music_manager.ready_to_shoot();
			}
		}

		// Update the input sign from this frame
		if (prev_vert_input_sign > 0 && defense_vertical_input <= 0) {
			prev_vert_input_sign = 0;
		} else if (prev_vert_input_sign < 0 && defense_vertical_input >= 0) {
			prev_vert_input_sign = 0;
		}

		// Reset moved_this_beat
		if (!music_manager.is_valid_move_frame()) {
			moved_this_beat = false;
		}
	}

	// Called when the player tries to change lanes with bad timing
	private void on_bad_lane_input() {
		print("bad defender lane input");
		// todo - handle bad timing for lane input, for defenders
	}
}
