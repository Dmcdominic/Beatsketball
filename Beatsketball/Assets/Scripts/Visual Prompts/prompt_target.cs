using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prompt_target : MonoBehaviour {
	public event_object player_failed;
	public int player_index;
	public Sprite sprite_normal;
	public Sprite sprite_down;

	private string player_string;
	private Image image;


	// Init
	private void Awake() {
		player_failed.addListener(on_fail);
		player_string = (player_index + 1).ToString();
		image = GetComponent<Image>();
	}

	// Show a red X, cuz you failed
	private void on_fail() {
		GetComponent<Animator>().SetTrigger("failed");
	}

	private void Update() {
		foreach (string key in key_prompts.keys) {
			if (Time.timeScale > 0 && Input.GetAxisRaw(key + "_" + player_string) > 0) {
				image.sprite = sprite_down;
				return;
			}
		}
		image.sprite = sprite_normal;
	}
}
