/*****************************************************************************
// File Name :         GameControllerBehaviour.cs
// Author :            Andrew Krenzel (??%)
                       Doug Guzman (??%)
                       
// Creation Date :     2/13/2020
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerBehaviour : MonoBehaviour
{
    [Header("UI Object References")]
    [Tooltip("The text that displays the player's remaining health")]
    public Text healthText; // AK
    [Tooltip("The text that displays the number of lives the player has left")]
    public Text livesText; // AK
    [Tooltip("The number of the room the player is currently in")]
    public Text roomNumberText; // AK

    // References to the player's stats
    private int health; // AK
    private int lives; // AK

    private GameObject player;
    
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        GetComponents();
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        UpdateUI();
        
    }

    private void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void GetComponents() // AK
    {
        player = GameObject.Find("Player");
    }
    private void UpdateUI() // AK
    {
        health = player.GetComponent<BasePlayerBehaviour>().health; // AK
        healthText.text = "Health Remaining: " + health + "%"; // AK

        lives = player.GetComponent<BasePlayerBehaviour>().lives; // AK
        livesText.text = "Lives Remaining: " + lives; // AK
    }
}