﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_control : MonoBehaviour {

	private float initial_size;
    public GameObject player1;
    public GameObject player2;
    private float player_x;

	// Init
	void Awake() {
		initial_size = Camera.main.orthographicSize;
	}


	// Update is called once per frame
	void Update() {
		// Zoom based on Time.timeScale
		Camera.main.orthographicSize = initial_size / Time.timeScale;

        //// get the x position of the offensive player
        //if (player1.activeSelf == true)
        //{
        //    player_x = player1.transform.position.x;
        //} else if (player2.activeSelf == true)
        //{
        //    player_x = player2.transform.position.x;
        //}


        //// set the x positon of the camera
        //if (music_manager.facing_off == false)
        //{
        //    transform.position = new Vector3(0, 0, -10);

        //    Camera.main.orthographicSize = 5;
        //}
        //else
        //{
        //    transform.position = new Vector3(player_x, -2, -10);

        //    Camera.main.orthographicSize = 2;
        //}
    }
}
