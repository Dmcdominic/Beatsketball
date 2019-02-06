using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteSpawner : MonoBehaviour
{
    public GameObject note_prefab;

    public event_object on_beat_event;

    // Start is called before the first frame update
    void Start()
    {
        on_beat_event.addListener(Spawn);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Spawn()
    {
        Instantiate(note_prefab, transform.position, transform.rotation);
    }

   

}
