/*****************************************************************************
// File Name :         BackButton.cs
// Author :            Shane Jackson (100%)
// Creation Date :     4/09/2020
//
// Brief Description : Controls the back button in any situation that where
                        the player navigates to a scene from the main menu.
*****************************************************************************/using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void backButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
