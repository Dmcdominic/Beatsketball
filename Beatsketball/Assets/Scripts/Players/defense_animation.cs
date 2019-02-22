using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defense_animation : MonoBehaviour {
	public int team = 0;

	// Update is called once per frame
	void Update() {
		//GetComponent<Animator>().SetBool("facing_off", music_manager.facing_off);
		GetComponent<Animator>().SetBool("dabbing", music_manager.facing_off && music_manager.completing_faceoff && music_manager.faceoff_winner == team);
		GetComponent<Animator>().SetBool("sagging", music_manager.facing_off && music_manager.completing_faceoff && music_manager.faceoff_winner != team);
	}
}
