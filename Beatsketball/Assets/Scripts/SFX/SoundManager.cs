using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {
	// AudioSources
	public AudioSource airhorn;
	public AudioSource buzzer;
	public AudioSource ball_bounce;
	public AudioSource shuffle_1;
	public AudioSource shuffle_2;
	public AudioSource swish;

	public static SoundManager instance = null;

	public float airhorn_chain_odds;
	public float airhorn_chain_delay;

	public float swish_delay;

	public float lowPitchRange;
	public float highPitchRange;


	// Static instance setup and initialization
	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
			return;
		}

		//DontDestroyOnLoad(gameObject);
		SceneManager.activeSceneChanged += onSceneChanged;
	}

	private void onSceneChanged(Scene current, Scene next) {
		//if (next.buildIndex < 2 && !titleTrack.isPlaying) {
		//	titleTrack.Play();
		//	lvlTrack.Stop();
		//} else if (next.buildIndex >= 2 && !lvlTrack.isPlaying) {
		//	titleTrack.Stop();
		//	lvlTrack.Play();
		//}
	}


	public void playAirhorn() {
		airhorn.pitch = Random.Range(lowPitchRange, highPitchRange);
		airhorn.Play();
		StartCoroutine(airhorn_chain());
	}

	private IEnumerator airhorn_chain() {
		yield return new WaitForSecondsRealtime(airhorn_chain_delay);
		if (Random.Range(0, 1f) < airhorn_chain_odds) {
			playAirhorn();
		}
	}

	public void playBuzzer() {
		buzzer.pitch = Random.Range(lowPitchRange, highPitchRange);
		buzzer.Play();
	}

	public void playBallBounce() {
		//ball_bounce.pitch = Random.Range(lowPitchRange, highPitchRange);
		ball_bounce.Play();
	}

	public void playShuffle() {
		if (Random.Range(0, 1f) < 0.5f) {
			shuffle_1.pitch = Random.Range(lowPitchRange, highPitchRange);
			shuffle_1.Play();
		} else {
			shuffle_2.pitch = Random.Range(lowPitchRange, highPitchRange);
			shuffle_2.Play();
		}
	}

	public void playSwish() {
		swish.pitch = Random.Range(lowPitchRange, highPitchRange);
		swish.Play();
	}

}
