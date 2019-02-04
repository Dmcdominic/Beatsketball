using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum frame_result { good, fail, none };

public class faceoff : MonoBehaviour {

	public float timescale_increment;

	public static bool facing_off = false;
	public static bool p1_pass = false;
	public static bool p2_pass = false;
	public static string p1_button = "A";
	public static string p2_button = "A";
	public static List<string> buttons = new List<string> { "A", "B", "X", "Y" };


	private void Start() {
		init_new_buttons();
	}

	// Set up a new pair of buttons
	private void init_new_buttons() {
		p1_button = buttons[Random.Range(0, buttons.Count - 1)];
		p2_button = buttons[Random.Range(0, buttons.Count - 1)];
		facing_off = true;
	}

	// Time is up for this button pair. Check the results.
	private void check_result() {
		if (p1_pass && p2_pass) {
			init_new_buttons();
		} else if (!p1_pass && !p2_pass) {
			init_new_buttons();
		} else if (p1_pass) {
			// P1 won
		} else {
			// P2 won
		}
	}

	// If you are facing off, steadily increase the timescale
	private void FixedUpdate() {
		if (!facing_off) {
			return;
		}
		Time.timeScale += timescale_increment * Time.fixedUnscaledDeltaTime;

		frame_result p1_result = check_player_input(1);
		frame_result p2_result = check_player_input(2);
		bool is_valid_frame = music_manager.is_valid_frame();


		// todo - more logic here to determine if someone failed and someone else succeeded (but has to account for missing completely)
		if (is_valid_frame && p1_result == frame_result.good) {
			// p1 landed their button
		}

		if (is_valid_frame && p2_result == frame_result.good) {
			// p2 landed their button
		}
	}

	private frame_result check_player_input(int player) {
		string current_button = (player == 1) ? p1_button : p2_button;
		string postfix = (player == 1) ? "_1" : "_2";
		bool good = false;

		foreach (string Button in buttons) {
			if (Input.GetButtonDown(Button + "_1")) {
				if (Button == current_button) {
					good = true;
				} else {
					return frame_result.fail;
				}
			}
		}

		frame_result result = good ? frame_result.good : frame_result.none;
		return result;
	}

}
