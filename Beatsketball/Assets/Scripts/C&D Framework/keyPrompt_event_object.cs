using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "events/keyPrompt_event")]
public class keyPrompt_event_object : ScriptableObject {
	[SerializeField]
	key_prompt constant = new key_prompt();

	public class keyPrompt_event : UnityEvent<key_prompt> {}

	public UnityEvent<key_prompt> e = new keyPrompt_event();

	public void Invoke(key_prompt d) {e.Invoke(d);}

	public void Invoke() {e.Invoke(constant);}

}

