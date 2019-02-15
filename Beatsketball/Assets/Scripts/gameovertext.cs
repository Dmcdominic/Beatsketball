using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameovertext : MonoBehaviour
{
    public Text gameover;
    public Text winnerName;


    // Start is called before the first frame update
    void Start()
    {
        gameover.text = "GAME OVER!";

    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<gameManager>().gameHasEnded == true)
        {
            if (music_manager.p1_score > music_manager.p2_score)
            {
                winnerName.text = "PLAYER 1 WON!";
            }
            else if (music_manager.p1_score < music_manager.p2_score)
            {
                winnerName.text = "PLAYER 2 WON!";
            }
            else
            {
                winnerName.text = "GAME TIED!";
            }
        }

    }
}
