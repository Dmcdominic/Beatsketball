using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offense_script : MonoBehaviour
{
    public GameObject offense;
    public float speed = 2.0F;

    private int lane = 2;
    private int counter = 0;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 forward = Vector3.zero;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
    controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float p1_vertical_input = Input.GetAxis("Vertical_1");

        moveDirection = new Vector3(0, 2.5F, 0);
        forward = new Vector3(0.025F, 0, 0);

        offense.transform.position += forward;

        // counter for lanes
        counter += 1;

        // switch lanes
        if (music_manager.is_valid_big_frame() == true)
        {
            if (p1_vertical_input > 0 && lane > 1 && counter >= 30)
            {
                offense.transform.position += moveDirection;
                lane -= 1;
                counter = 0;
            }

            if (p1_vertical_input < 0 && lane < 3 && counter >= 30)
            {
                offense.transform.position -= moveDirection;
                lane += 1;
                counter = 0;
            }
        }
    }

    // enter faceoff
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Defense")
        {
            offense.transform.position = new Vector3(-6, 0, 49);
        }

    }
}
