using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XboxCtrlrInput;

public class event_system_xbox : MonoBehaviour {

	private bool stickLeft;
	private bool stickRight;
	private bool stickUp;
	private bool stickDown;

	private static readonly float input_min = 0.3f;


	// Initialization
	private void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update() {
		if (!EventSystem.current || !EventSystem.current.currentSelectedGameObject) {
			return;
		}

		GameObject selected_GO = EventSystem.current.currentSelectedGameObject;
		Selectable selected = selected_GO.GetComponent<Selectable>();
		if (!selected || !selected.isActiveAndEnabled) {
			return;
		}

		// A to select
		if (XCI.GetButtonDown(XboxButton.A)) {
			Button selected_button = selected.GetComponent<Button>();
			if (selected_button) {
				selected_button.onClick.Invoke();
			}
		}

		// Check for fresh stick input
		bool stickUp_just_now = !stickUp && XCI.GetAxis(XboxAxis.LeftStickY, XboxController.Any) > input_min;
		bool stickDown_just_now = !stickDown && XCI.GetAxis(XboxAxis.LeftStickY, XboxController.Any) < -input_min;
		bool stickLeft_just_now = !stickLeft && XCI.GetAxis(XboxAxis.LeftStickX, XboxController.Any) < -input_min;
		bool stickRight_just_now = !stickRight && XCI.GetAxis(XboxAxis.LeftStickX, XboxController.Any) > input_min;

		Selectable next_selected = null;

		// Stick or D-Pad to navigate
		if (stickUp_just_now || XCI.GetButtonDown(XboxButton.DPadUp, XboxController.Any)) {
			next_selected = selected.FindSelectableOnUp();
		}
		if (stickDown_just_now || XCI.GetButtonDown(XboxButton.DPadDown, XboxController.Any)) {
			next_selected = selected.FindSelectableOnDown();
		}
		if (stickLeft_just_now || XCI.GetButtonDown(XboxButton.DPadLeft, XboxController.Any)) {
			next_selected = selected.FindSelectableOnLeft();
		}
		if (stickRight_just_now || XCI.GetButtonDown(XboxButton.DPadRight, XboxController.Any)) {
			next_selected = selected.FindSelectableOnRight();
		}

		// Select the new selectable, if there was input
		if (next_selected && next_selected != selected.gameObject) {
			EventSystem.current.SetSelectedGameObject(next_selected.gameObject);
		}

		// Update bools for this frame
		stickUp 	= XCI.GetAxis(XboxAxis.LeftStickY, XboxController.Any) > input_min;
		stickDown 	= XCI.GetAxis(XboxAxis.LeftStickY, XboxController.Any) < -input_min;
		stickLeft 	= XCI.GetAxis(XboxAxis.LeftStickX, XboxController.Any) < -input_min;
		stickRight 	= XCI.GetAxis(XboxAxis.LeftStickX, XboxController.Any) > input_min;
	}
}
