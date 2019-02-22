using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class action_lines : MonoBehaviour {

	public GameObject action_lines_sprite;
    //public GameObject offense1;
    //public GameObject offense2;

    private GameObject player;

	private Vector2 init_pos;
	private Vector3 init_scale;


	private void Start() {
		init_pos = action_lines_sprite.transform.position;
		init_scale = action_lines_sprite.transform.localScale;
	}

	// Update the action lines every frame based on face off status
	void Update() {
		if (!music_manager.facing_off || music_manager.completing_faceoff) {
			action_lines_sprite.SetActive(false);
			return;
		}

        //Debug.Log(Time.timeScale);

        //if (offense1.activeSelf == true)
        //{
        //    player = offense1;
        //}
        //else
        //{
        //    player = offense2;
        //}

        action_lines_sprite.SetActive(true);
		float y_range = Mathf.Sqrt(Time.timeScale - 1f) * 0.3f;
		float x_range = Mathf.Sqrt(Time.timeScale - 1f) * 0.5f;

		float new_y_pos = init_pos.y + Random.Range(-y_range, y_range);
		float new_x_pos = init_pos.x + Random.Range(-x_range, x_range);
		action_lines_sprite.transform.position = new Vector2(new_x_pos, new_y_pos);
		action_lines_sprite.transform.localScale = init_scale / Time.timeScale;

		//float new_y_pos = player.transform.position.y + Random.Range(-y_range, y_range);
		//float new_x_pos = player.transform.position.x + Random.Range(-x_range, x_range);
		//action_lines_sprite.transform.position = new Vector2(new_x_pos, new_y_pos);
		//      if (Time.timeScale <= 1.5f)
		//      {
		//          action_lines_sprite.transform.localScale = init_scale / (Time.timeScale * 1.5f);
		//      }
		//      else
		//      {
		//          action_lines_sprite.transform.localScale = init_scale / (1.5f *1.5f);
		//      }
	}
}
