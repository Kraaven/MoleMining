using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScript : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject controlsMenu;
    public GameObject BacktoResume;
    public bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                print("escape is pressed");
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Controls()
    {
        controlsMenu.SetActive(true);
        BacktoResume.SetActive(false);
    }

    public void Back()
    {
        controlsMenu.SetActive(false);
        BacktoResume.SetActive(true);
    }
}
