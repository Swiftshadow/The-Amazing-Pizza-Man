/*****************************************************************************
// File Name :         GameControllerBehaviour.cs
// Author :            Andrew Krenzel
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerBehaviour : MonoBehaviour
{
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchScene("WorldGenTest");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchScene("PlayerTestingForGrappling");
        }
        
    }

    private void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}