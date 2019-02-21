using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="track")]
public class track : ScriptableObject {

	public AudioClip song;
	public string title;
	public string artist;
	public float bpm;
	public float start_time;

	public float preview_start_time;

	[Range(0, 1)]
	public float volume = 0.6f;

	public bool single_airhorn_override = false;
	public Sprite album_cover;

}
