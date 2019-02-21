using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_anim : MonoBehaviour {

	public event_object big_beat_event;

	private bool just_had_full_beat = false;
	private Animator animator;


	// Start is called before the first frame update
	void Start() {
		animator = GetComponent<Animator>();
		animator.SetFloat("speed", 0.25f / music_manager.Music_Manager.beat_interval);
		big_beat_event.addListener(on_big_beat);
	}

	// Called on every big beat
	private void on_big_beat() {
		if (just_had_full_beat) {
			just_had_full_beat = false;
			return;
		}

		on_full_beat();
		just_had_full_beat = true;
	}

	// Called on every FULL beat (every 2 big beats)
	private void on_full_beat() {
		animator.SetTrigger("flash on");
	}
}
