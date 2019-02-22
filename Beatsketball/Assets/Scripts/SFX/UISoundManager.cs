using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISoundManager : MonoBehaviour
{
    public AudioSource ball_dribble;
    public AudioSource ball_fumbled;
    public AudioSource nylon_swish;
    public AudioSource game_start_sound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playBallDribble()
    {
        ball_dribble.Play();
    }

    public void playBallFumbled()
    { 
        ball_fumbled.Play();
    }

    public void playNylonSwish()
    {
        nylon_swish.Play();
    }

    public void playGameStart()
    {
        game_start_sound.Play();
    }
}
