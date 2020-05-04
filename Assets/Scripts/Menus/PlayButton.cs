using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void playGame()
    {
        Time.timeScale = 1;
        NextLevel.SetLevelsComplete(0);
        PauseMenu.gamePaused = false;
        SceneManager.LoadScene("MasterScene");
    }

    public void howTo()
    {
        SceneManager.LoadScene("HowTo");
    }

    public void credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
