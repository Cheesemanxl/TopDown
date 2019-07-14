using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Canvas main;
    public Canvas howTo;

    public void Start()
    {
        howTo.enabled = false;
    }

    public void BeginGame()
    {
        SceneManager.LoadScene(1);
    }

    public void HowToOpen()
    {
        main.enabled = false;
        howTo.enabled = true;
    }

    public void HowToClose()
    {
        howTo.enabled = false;
        main.enabled = true;
        
    }

    public void QuitGame()
    {
        Application.Quit();

    }

}
