using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialControl : MonoBehaviour
{
    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;

    // Start is called before the first frame update
    void Start()
    {
        pause();
        tutorial1.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void load_tutorial2()
    {
        tutorial1.gameObject.SetActive(false);
        tutorial2.gameObject.SetActive(true);
    }

    public void load_tutorial3()
    {
        tutorial2.gameObject.SetActive(false);
        tutorial3.gameObject.SetActive(true);
    }

    public void startgame()
    {
        tutorial3.gameObject.SetActive(false);
        resume();
    }

    void pause()
    {
        Time.timeScale = 0f;
    }

    void resume()
    {
        Time.timeScale = 1f;
    }
}
