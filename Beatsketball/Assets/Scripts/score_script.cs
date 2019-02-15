using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score_script : MonoBehaviour
{
    public Text p1_score;
    public Text p2_score;

    public Text timer;

    private float timer_var;
    private int timer_min;
    private float timer_sec;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer_var = music_manager.song_time_remaining();
        timer_min = Mathf.FloorToInt(timer_var)/60;
        timer_sec = timer_var % 60;

        p1_score.text = "Player 1: " + music_manager.p1_score;
        p2_score.text = "Player 2: " + music_manager.p2_score;

        if (timer_sec < 10)
        {
            timer.text = timer_min + ":0" + timer_sec.ToString("F1");
        }
        else
        {
            timer.text = timer_min + ":" + timer_sec.ToString("F1");
        }



        if (System.Math.Abs(timer_var) < 0.1)
        {
            FindObjectOfType<gameManager>().EndGame();

        }



    }

}
