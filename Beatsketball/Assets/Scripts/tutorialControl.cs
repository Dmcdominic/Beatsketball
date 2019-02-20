using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tutorialControl : MonoBehaviour {
	public GameObject tutorial1;
	public GameObject tutorial2;
	public GameObject tutorial3;
	public GameObject next_button1;
	public GameObject next_button2;
	public GameObject next_button3;

	// Start is called before the first frame update
	void Start() {
		pause();
		tutorial1.gameObject.SetActive(true);
		EventSystem.current.SetSelectedGameObject(next_button1);
	}

	public void load_tutorial2() {
		tutorial1.gameObject.SetActive(false);
		tutorial2.gameObject.SetActive(true);
		EventSystem.current.SetSelectedGameObject(next_button2);
	}

	public void load_tutorial3() {
		tutorial2.gameObject.SetActive(false);
		tutorial3.gameObject.SetActive(true);
		EventSystem.current.SetSelectedGameObject(next_button3);
	}

	public void startgame() {
		tutorial3.gameObject.SetActive(false);
		resume();
	}

	void pause() {
		Time.timeScale = 0f;
	}

	void resume() {
		Time.timeScale = 1f;
	}
}
