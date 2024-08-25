using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenuScript : MonoBehaviour
{
// This method loads the game scene, replace "GameScene" with your actual scene name
    public void PlayGame()
    {
        SceneManager.LoadScene("Player movement");
    }

    // This method can be expanded to open a settings menu
    public void OpenSettings()
    {
        // Code to open the settings menu
        Debug.Log("Settings button pressed");
    }

    // This method quits the game
    public void QuitGame()
    {
        Debug.Log("Quit button pressed");
        Application.Quit();
    }

}