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
        //only show main menu when loaded
        howTo.enabled = false;
    }

    //load the main game scene when the button is clicked
    public void BeginGame()
    {
        SceneManager.LoadScene(1);
    }

    //enable how to play menu and close main menu
    public void HowToOpen()
    {
        main.enabled = false;
        howTo.enabled = true;
    }

    //enable main menu and close how to play menu
    public void HowToClose()
    {
        howTo.enabled = false;
        main.enabled = true;
        
    }

    //close the application
    public void QuitGame()
    {
        Application.Quit();

    }

}
