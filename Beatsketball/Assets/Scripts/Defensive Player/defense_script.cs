using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defense_script : MonoBehaviour
{
    public GameObject defense1;
    public GameObject defense2;
    public GameObject defense3;
    public GameObject offense;

    private int lane = 2;
    private int counter = 0;
    private GameObject defense;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        // establish defensive player
        defense = defense1;
    }

    // Update is called once per frame
    void Update()
    {
        float p2_vertical_input = Input.GetAxis("Vertical_2");

        moveDirection = new Vector3(0, 0.8333F, 0);

        // counter for lanes
        counter += 1;

        // switch lanes
        if (music_manager.is_valid_big_frame())
        {
            if (p2_vertical_input > 0 && lane > 1 && counter >= 30)
            {
                defense.transform.position += moveDirection;
                lane -= 1;
                counter = 0;
            }

            if (p2_vertical_input < 0 && lane < 3 && counter >= 30)
            {
                defense.transform.position -= moveDirection;
                lane += 1;
                counter = 0;
            }
        }

        // switch defensive player
        if (offense.transform.position.x > defense.transform.position.x)
        {
            if (defense == defense1)
            {
                defense = defense2;
            } else if (defense == defense2)
            {
                defense = defense3;
            }
        }
    }

    // enter faceoff
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Offense")
        {
            //enter faceoff
        }

    }
}
