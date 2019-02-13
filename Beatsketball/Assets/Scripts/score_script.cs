using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score_script : MonoBehaviour
{
    public Text p1_score;
    public Text p2_score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        p1_score.text = "Player 1: " + music_manager.p1_score;
        p2_score.text = "Player 2: " + music_manager.p2_score;
    }
}
