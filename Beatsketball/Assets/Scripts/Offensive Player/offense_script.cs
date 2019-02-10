using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offense_script : MonoBehaviour
{
    public GameObject offense;
    public float speed = 2.0F;

    private bool hasBall = true;
    private int lane = 2;

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
        float p1_vertical_input = Input.GetAxisDown("Vertical_1");

        moveDirection = new Vector3(0, 0, 0.25F);
        forward = new Vector3(0.025F, 0, 0);

        offense.transform.position += forward;

        if (music_manager.is_valid_big_frame() == true)
        {
            if (p1_vertical_input > 0 && lane > 1)
            {
                offense.transform.position += moveDirection;
                lane -= 1;
            }

            if (p1_vertical_input < 0 && lane < 3)
            {
                offense.transform.position -= moveDirection;
                lane += 1;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Defense")
        {
            offense.transform.position = new Vector3(-5, -0.5F, -12.5F);
            //Enter Face Off
        }
    }
}
