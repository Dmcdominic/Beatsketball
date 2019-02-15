using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameovertext : MonoBehaviour {
	public Text gameover;
	public Text winnerName;


	// Start is called before the first frame update
	void Start() {
		gameover.text = "GAME OVER!";
		if (FindObjectOfType<gameManager>().winner == 1) {
			winnerName.text = "PLAYER 1 WON!";
		} else if (FindObjectOfType<gameManager>().winner == 2) {
			winnerName.text = "PLAYER 2 WON!";
		} else {
			winnerName.text = "GAME TIED!";
		}

	}

	// Update is called once per frame
	void Update() {

	}
}
