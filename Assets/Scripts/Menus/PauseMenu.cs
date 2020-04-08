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
            PauseGame();
        }
    }
    void PauseGame()
    {
        gamePaused = !gamePaused;
        if (gamePaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void continueButton()
    {
        Time.timeScale = 1;
        PauseGame();
    }

    public void quitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void restartButton()
    {
        SceneManager.LoadScene("WorldGenTest");
    }
}