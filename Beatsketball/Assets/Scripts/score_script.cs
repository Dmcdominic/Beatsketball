using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score_script : MonoBehaviour {
	public Text p1_score;
	public Text p2_score;

	public Text timer;

	public Animator p1_score_flashing_light;
	public Animator p2_score_flashing_light;

	public int_event_object player_scored_event;

	private float timer_var;
	private int timer_min;
	private float timer_sec;



	// Start is called before the first frame update
	void Start() {
		player_scored_event.e.AddListener(on_player_scored);
	}

	// Update is called once per frame
	void Update() {
		timer_var = music_manager.song_time_remaining();
		timer_min = Mathf.FloorToInt(timer_var) / 60;
		timer_sec = timer_var % 60;

		p1_score.text = music_manager.p1_score.ToString();
		p2_score.text = music_manager.p2_score.ToString();

		if (timer_sec < 10) {
			timer.text = timer_min + ":0" + timer_sec.ToString("F1");
		} else {
			timer.text = timer_min + ":" + timer_sec.ToString("F1");
		}

		if (System.Math.Abs(timer_var) < 0.1) {
			FindObjectOfType<gameManager>().EndGame();

		}
	}

	// Called whenever a player scores.
	// Triggers the score flashers.
	private void on_player_scored(int player_index) {
		if (player_index == 0) {
			p1_score_flashing_light.SetTrigger("flash");
		} else {
			p2_score_flashing_light.SetTrigger("flash");
		}
	}

}
