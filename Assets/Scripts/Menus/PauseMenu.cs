/*****************************************************************************
// File Name :         PauseMenu.cs
// Author :            Shane Jackson (100%)
// Creation Date :     4/09/2020
//
// Brief Description : Controls the pause menu and all subsequent buttons 
                        therein. 
*****************************************************************************/
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
        PauseGame();
    }

    public void quitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void restartButton()
    {

        Time.timeScale = 1;
        gamePaused = false;
        NextLevel.SetLevelsComplete(0);
        SceneManager.LoadScene("MasterScene");
    }
}