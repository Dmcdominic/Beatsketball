using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beat_tester : MonoBehaviour {

	public event_object on_beat_event;

	// Start is called before the first frame update
	void Start() {
		on_beat_event.addListener(on_beat);
	}

	private void on_beat() {
		Vector3 pos = transform.position;
		transform.position = pos - (Vector3)(Vector2.right * 2 * pos.x);
	}
}
