using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="track")]
public class track : ScriptableObject {

	public AudioClip song;
	public float bpm;
	public float start_time;

}
