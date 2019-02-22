using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class complete_dunk : MonoBehaviour {

	public int team = 0;

	private bool dunking = false;

	private Vector3 init_pos;

	private offense_script player;


	private void Awake() {
		init_pos = transform.position;
		player = GetComponentInParent<offense_script>();
	}

	private void OnEnable() {
		dunking = false;
	}

	private void Update() {
		if (!dunking && music_manager.shooting == shooting_state.shot) {
			transform.localScale = new Vector3(2.5f, 2.5f, 0);
			player.transform.localScale = new Vector3(1, 1, 1);
			player.transform.position = player.initial_position;
			GetComponent<Animator>().SetTrigger("dunk");
			dunking = true;
			return;
		} else if (!dunking) {
			transform.position = init_pos;
		}

		GetComponent<Animator>().SetBool("facing_off", music_manager.facing_off);
		GetComponent<Animator>().SetBool("dabbing", music_manager.facing_off && music_manager.completing_faceoff && music_manager.faceoff_winner == team);
		GetComponent<Animator>().SetBool("sagging", music_manager.facing_off && music_manager.completing_faceoff && music_manager.faceoff_winner != team);
	}

	public void on_complete_dunk() {
		music_manager.Music_Manager.finish_shooting_ball(team);
	}
}
