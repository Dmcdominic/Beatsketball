using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {
	public bool gameHasEnded = false;
	public int winner;


	public void EndGame() {
		if (gameHasEnded == false) {
			gameHasEnded = true;
			Debug.Log("Game Over");
		}
	}

	public void findWinner() {
		if (FindObjectOfType<score_script>().player1win == true) {
			winner = 1;
		} else if (FindObjectOfType<score_script>().player2win == true) {
			winner = 2;
		} else {
			winner = 0;
		}
	}
}
