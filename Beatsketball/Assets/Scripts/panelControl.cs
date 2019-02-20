using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class panelControl : MonoBehaviour {
	public GameObject gameOverPanel;
	public GameObject main_menu_button;

	// Start is called before the first frame update
	void Start() {
		gameOverPanel.SetActive(false);
	}

	// Update is called once per frame
	void Update() {
		if (FindObjectOfType<gameManager>().gameHasEnded == true && !gameOverPanel.activeInHierarchy) {
			gameOverPanel.SetActive(true);
			EventSystem.current.SetSelectedGameObject(main_menu_button);
		}
	}

	public void MainMenu() {
		SceneManager.LoadScene(1);
	}
}
