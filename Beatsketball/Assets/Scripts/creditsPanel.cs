using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsPanel : MonoBehaviour
{
    public GameObject credits_panel;
    bool credits_on;

    // Start is called before the first frame update
    void Start()
    {
        credits_panel.SetActive(false);
        credits_on = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showCredits()
    {
        if (credits_on == false)
        {
            credits_panel.SetActive(true);
            credits_on = true;
        }
        else
        {
            credits_panel.SetActive(false);
            credits_on = false;
        }

    }
}
