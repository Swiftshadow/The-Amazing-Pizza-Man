using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            gamePaused = !gamePaused;
            PauseGame();
        }
    }
    void PauseGame()
    {
        if (gamePaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void continueButton()
    {
        Time.timeScale = 1;
    }

    public void quitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}