using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="master music")]
public class master_music : ScriptableObject {

	public track current_track;
	[Range(0, 1)]
	public float master_volume = 1;

	public List<track> all_tracks;

}
