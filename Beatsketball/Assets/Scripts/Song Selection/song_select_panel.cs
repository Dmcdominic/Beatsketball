using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class song_select_panel : MonoBehaviour {

	public master_music master_Music;
	public AudioSource song_preview_player;
	public song_select_button Song_Select_Button_prefab;
	
	public float song_preview_duration;
	public Color current_selected_color;

	private List<song_select_button> song_Select_Buttons;


	// Start is called before the first frame update
	void Start() {
		song_Select_Buttons = new List<song_select_button>();

		foreach(track Track in master_Music.all_tracks) {
			song_select_button new_song_button = Instantiate(Song_Select_Button_prefab, transform);
			new_song_button.Track = Track;
			new_song_button.song_Select_Panel = this;
			set_selected(new_song_button, false);
			song_Select_Buttons.Add(new_song_button);
		}

		// Set song to default (first one), and preview it
		song_select_button default_song = song_Select_Buttons[0];
		master_Music.current_track = default_song.Track;
		Button button = default_song.GetComponent<Button>();
		set_color(button, current_selected_color);
		set_selected(default_song, true);
		StartCoroutine(play_song_preview(default_song.Track, true));
	}

	// Called when a song_select_button is pressed
	public void on_song_select(song_select_button selected_button) {
		// Update master_Music
		master_Music.current_track = selected_button.Track;

		// Play the preview
		StopAllCoroutines();
		StartCoroutine(play_song_preview(selected_button.Track));

		// Update coloring
		foreach (song_select_button song_button in song_Select_Buttons) {
			set_color(song_button.button, Color.clear);
			set_selected(song_button, false);

		}
		set_color(selected_button.button, current_selected_color);
		set_selected(selected_button, true);
	}

	// Set the color of a button. Currently unused, replaced by the "selected" cursor
	private void set_color(Button button, Color color) {
		//ColorBlock newColorBlock = button.colors;
		//newColorBlock.normalColor = color;
		//button.colors = newColorBlock;
	}

	// Set the "selected" cursor of a given song button
	private void set_selected(song_select_button song_button, bool selected) {
		song_button.selected_cursor.enabled = selected;
	}

	// Play the song preview, managing fade-in and fade-out
	IEnumerator play_song_preview(track Track, bool extra_delay = false) {
		float fade_in_duration = 0.5f;
		float fade_out_duration = 1.5f;

		// Extra delay before starting, used for default song preview
		if (extra_delay) {
			yield return new WaitForSeconds(0.1f);
		}

		// Setup
		song_preview_player.Stop();
		song_preview_player.clip = Track.song;
		song_preview_player.time = Track.preview_start_time;
		song_preview_player.volume = 0;
		song_preview_player.Play();

		float volume = master_Music.master_volume * Track.volume;

		// Fade in
		while (song_preview_player.volume < volume) {
			song_preview_player.volume += Time.deltaTime / fade_in_duration * volume;
			if (song_preview_player.volume > volume) {
				song_preview_player.volume = volume;
			}
			yield return null;
		}

		// Wait
		yield return new WaitForSeconds(song_preview_duration);

		// Fade out
		while (song_preview_player.volume > 0) {
			song_preview_player.volume -= Time.deltaTime / fade_out_duration * volume;
			yield return null;
		}

		song_preview_player.Stop();
	}
}
