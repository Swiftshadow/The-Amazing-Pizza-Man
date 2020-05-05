/*****************************************************************************
// File Name :         SceneLoader.cs
// Author :            Doug Guzman
                       
// Creation Date :     4/10/2020
//
// Brief Description : Loads a scene given the name 
*****************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
