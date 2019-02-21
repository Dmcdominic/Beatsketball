using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offense_script : MonoBehaviour {
	public int player_set_index;
	public GameObject offense;
	public GameObject other_offense;

	public static int lane = 1;
	private int prev_vert_input_sign = 0;

	[HideInInspector]
	public Vector3 initial_position;
	private Vector3 initial_scale;

	// True iff a player just scored OR a match just started
	//	(meaning offense position needs to reset)
	public static bool player_just_scored = true;
	public static bool enabled_this_frame = false;

	public static float speed;

	public static readonly float starting_speed = 0.7f;
	public static readonly float good_speed_incr = -0.08f;
	public static readonly float great_speed_incr = 0f;
	public static readonly float perfect_speed_incr = 0.1f;
	public static readonly float shooting_speed_slowdown = 1.2f;
	public static readonly float min_speed = 0.25f;

	public static readonly float lane_delta_height = 0.9f;
	public static readonly float distance_scale_diff = 0.15f;
	public static readonly Vector3 moveDirection = new Vector3(0, lane_delta_height, 0);


	// Init
	public void Awake() {
		initial_position = transform.position;
		initial_scale = transform.localScale;
		if (player_set_index == 0) {
			player_just_scored = true;
		}
	}

	// Re-init the offense player
	private void OnEnable() {
		speed = starting_speed;
		if (player_just_scored) {
			offense.transform.position = initial_position;
			offense.transform.localScale = initial_scale;
			lane = 1;
			player_just_scored = false;
		} else if (!enabled_this_frame) {
			offense.transform.position = other_offense.transform.position;
			offense.transform.localScale = other_offense.transform.localScale;
		}
		enabled_this_frame = true;
		prev_vert_input_sign = 0;
	}

	// Update is called once per frame
	void Update() {
		enabled_this_frame = false;
		if (!music_manager.playing || music_manager.facing_off || music_manager.shooting == shooting_state.shot) {
			return;
		}

		// Move forward down the court
		Vector3 forward = new Vector3(speed, 0, 0);
		offense.transform.position += forward * Time.deltaTime * (player_set_index == 1 ? -1f : 1f);

		// Take in the input and determine if you should change lanes
		string input_axis = "Vertical_" + (player_set_index + 1).ToString();
		float offense_vertical_input = Input.GetAxisRaw(input_axis);

		if (offense_vertical_input > 0 && prev_vert_input_sign <= 0 && lane > 0) {
			if (music_manager.is_valid_move_frame()) {
				lane -= 1;
				offense.transform.position += moveDirection;
				offense.transform.localScale = initial_scale * ((1f - distance_scale_diff) + (distance_scale_diff * lane));
				prev_vert_input_sign = 1;
			} else {
				on_bad_lane_input();
			}
		}

		if (offense_vertical_input < 0 && prev_vert_input_sign >= 0 && lane < 2) {
			if (music_manager.is_valid_move_frame()) {
				lane += 1;
				offense.transform.position -= moveDirection;
				offense.transform.localScale = initial_scale * (0.9f + (0.1f * lane));
				prev_vert_input_sign = -1;
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

	// Adjust the offense speed based on a prompt that was just completed
	public static void adjust_speed(press_accuracy accuracy) {
		switch (accuracy) {
			case press_accuracy.good:
				speed += good_speed_incr;
				break;
			case press_accuracy.great:
				speed += great_speed_incr;
				break;
			case press_accuracy.perfect:
				speed += perfect_speed_incr;
				break;
		}
		if (speed < min_speed) {
			speed = min_speed;
		}
	}
}
