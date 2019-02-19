using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class panelControl : MonoBehaviour
{
    public GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<gameManager>().gameHasEnded == true) 
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
