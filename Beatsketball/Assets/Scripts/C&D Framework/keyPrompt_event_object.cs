using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "events/keyPrompt_event")]
public class keyPrompt_event_object : ScriptableObject {
	public class keyPrompt_event : UnityEvent<key_prompt, press_accuracy> {}

	public UnityEvent<key_prompt, press_accuracy> e = new keyPrompt_event();

	public void Invoke(key_prompt p, press_accuracy a) {e.Invoke(p, a);}

}

