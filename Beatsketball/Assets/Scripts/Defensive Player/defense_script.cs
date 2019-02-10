using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defense_script : MonoBehaviour
{
    public GameObject defense;

    private int lane = 2;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float p2_vertical_input = Input.GetAxis("Vertical_2");

        moveDirection = new Vector3(0, 0, 0.25F);

        if (music_manager.is_valid_big_frame())
        {
            if (p2_vertical_input > 0 && lane > 1)
            {
                defense.transform.position += moveDirection;
                lane -= 1;
            }

            if (p2_vertical_input < 0 && lane < 3)
            {
                defense.transform.position -= moveDirection;
                lane += 1;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Offense")
        {
            //Enter Face Off
        }
    }
}
