using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

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
		XboxController controller = player_index == 0 ? XboxController.First : XboxController.Second;
		foreach (string key in key_prompts.keys) {
			XboxButton button = XboxButton.A;
			switch (key) {
				case "A":
					button = XboxButton.A;
					break;
				case "B":
					button = XboxButton.B;
					break;
				case "X":
					button = XboxButton.X;
					break;
				case "Y":
					button = XboxButton.Y;
					break;
			}

			//if (Time.timeScale > 0 && Input.GetAxisRaw(key + "_" + player_string) > 0) {
			if (Time.timeScale > 0 && XCI.GetButtonDown(button, controller)) {
				image.sprite = sprite_down;
				return;
			}
		}
		image.sprite = sprite_normal;
	}
}
