using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    private UIScript ui;
    
    public void Start()
    {
        //get the uiscript from the ui object in the scene
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>();
    }

    //reloads scene when called by button press on the canvas
    public void RestartScene()
    {
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);

    }

    //Load main scene when called by button press on the canvas
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Close application when called by button press on the canvas
    public void QuitGame()
    {
        Application.Quit();

    }

}
