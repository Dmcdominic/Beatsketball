using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteControl : MonoBehaviour
{
    public float speed;
   

    // Start is called before the first frame update
    void Start()
    {
        speed = speed / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
       
    }
}
