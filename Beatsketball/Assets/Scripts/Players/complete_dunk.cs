using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class complete_dunk : MonoBehaviour {

	public int team = 0;

	private bool dunking = false;

	private Vector3 init_pos;


	private void Awake() {
		init_pos = transform.position;
	}

	private void OnEnable() {
		dunking = false;
	}

	private void Update() {
		if (!dunking && music_manager.shooting == shooting_state.shot) {
			//GetComponentInParent<offense_script>().Awake();
			GameObject player = GetComponentInParent<offense_script>().gameObject;
			transform.localScale = new Vector3(2.5f, 2.5f, 0);
			if (team == 0) {
				player.transform.localPosition = new Vector3(-7f, 0, 0);
			} else {
				player.transform.localPosition = new Vector3(7f, 0, 0);
			}
			player.transform.localScale = new Vector3(1, 1, 0);
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
