﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class complete_dunk : MonoBehaviour {

	public int team = 0;

	private bool dunking = false;

	private Vector3 init_pos;

	private GameObject player;
	private Vector3 init_player_pos;


	private void Awake() {
		init_pos = transform.position;
		player = GetComponentInParent<offense_script>().gameObject;
		init_player_pos = player.transform.position;
	}

	private void OnEnable() {
		dunking = false;
	}

	private void Update() {
		if (!dunking && music_manager.shooting == shooting_state.shot) {
			//transform.localScale = new Vector3(2.5f, 2.5f, 0);
			player.transform.localScale = new Vector3(1, 1, 1);
			player.transform.position = init_player_pos;
			GetComponent<Animator>().SetTrigger("dunk");
			dunking = true;
		} else if (!dunking) {
			transform.position = init_pos;
		}
	}

	public void on_complete_dunk() {
		music_manager.Music_Manager.finish_shooting_ball(team);
	}
}
