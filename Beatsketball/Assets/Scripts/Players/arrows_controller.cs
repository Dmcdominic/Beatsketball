using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrows_controller : MonoBehaviour {

	public int team;
	public bool defender;
	public int index;

	public GameObject up_arrow;
	public GameObject down_arrow;

	private int opposing_team_index;


	// Start is called before the first frame update
	void Start() {
		opposing_team_index = team == 1 ? 0 : 1;
	}

	// Update is called once per frame
	void Update() {
		if (!music_manager.is_valid_move_frame() || music_manager.facing_off) {
			update_arrows(false, false);
			return;
		}

		if (!defender) {
			// Offense case
			update_by_lane(offense_script.lane);
		} else {
			// Defense case
			if (defense_script.current_defender != index) {
				update_arrows(false, false);
			} else {
				int lane;
				if (opposing_team_index == 0) {
					lane = defense_script.set_0_lanes[index];
				} else {
					lane = defense_script.set_1_lanes[index];
				}
				//print("all_lanes == null: " + (defense_script.all_lanes == null));
				//print("all_lanes[oti] == null: " + (defense_script.all_lanes[opposing_team_index] == null));
				//print("all_lanes[oti][index] == null: " + (defense_script.all_lanes[opposing_team_index][index] == null));
				update_by_lane(lane);
			}
		}
	}

	// Update based on your lane
	private void update_by_lane(int lane) {
		if (lane == 0) {
			update_arrows(false, true);
		} else if (lane == 1) {
			update_arrows(true, true);
		} else if (lane == 2) {
			update_arrows(true, false);
		}
	}

	// Update arrow visuals
	private void update_arrows(bool up, bool down) {
		up_arrow.SetActive(up);
		down_arrow.SetActive(down);
	}
}
