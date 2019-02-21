using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class song_select_button : MonoBehaviour {

	public track Track;

	public Text title;
	public Text artist;
	public Text duration;
	public Text bpm;
	public Image album_cover;
	public Image selected_cursor;

	[HideInInspector]
	public song_select_panel song_Select_Panel;

	[HideInInspector]
	public Button button;


	// Init
	private void Awake() {
		button = GetComponent<Button>();
	}

	// Strings and album cover setup
	void Start() {
		title.text = Track.title;
		artist.text = Track.artist;
		duration.text = get_duration_string(Track.song.length);
		bpm.text = get_bpm_string(Track.bpm);
		album_cover.sprite = Track.album_cover;
	}

	// Returns the string to display the song duration
	private string get_duration_string(float length) {
		string minutes = Mathf.FloorToInt(length / 60f).ToString();
		string seconds = Mathf.FloorToInt(length % 60f).ToString();
		if (seconds.Length < 2) {
			seconds = "0" + seconds;
		}
		return "Duration - " + minutes + ":" + seconds;
	}

	// Returns the string to display the bpm
	private string get_bpm_string(float bpm) {
		return "BPM - " + Mathf.FloorToInt(bpm).ToString();
	}

	// Called when the player selects this song
	public void on_select() {
		song_Select_Panel.on_song_select(this);
	}

}
