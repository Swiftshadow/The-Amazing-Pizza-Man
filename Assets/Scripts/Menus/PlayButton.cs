using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("MasterScene");
    }

    public void howTo()
    {
        SceneManager.LoadScene("HowTo");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
