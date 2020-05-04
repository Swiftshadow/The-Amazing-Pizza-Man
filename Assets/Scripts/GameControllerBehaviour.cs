/*****************************************************************************
// File Name :         GameControllerBehaviour.cs
// Author :            Andrew Krenzel (75%)
                       Doug Guzman (25%)
                       
// Creation Date :     2/13/2020
//
// Brief Description : Controls the setting of UI elements, scene switching, and
                        the map power-up's arrow.
*****************************************************************************/

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerBehaviour : MonoBehaviour
{
    [Header("UI Object References")]
    [Tooltip("The text that displays the player's remaining health")]
    public Text healthText; // AK
    
    [Tooltip("Slider to display the player's health")]
    public Slider healthSlider;
    [Tooltip("The text that displays the number of lives the player has left")]
    public Text livesText; // AK
    //[Tooltip("The number of the room the player is currently in")]
    //public Text roomNumberText; // AK
    [Tooltip("The text displayed when the player dies and can respawn")]
    public Text respawnText; // AK
    [Tooltip("The text displayed when the player runs out of lives")]
    public Text gameOverText; // AK

    // References to the player's stats
    private int health; // AK
    private int lives; // AK

    private bool playerAlive;

    [Tooltip("The arrow on the minimap that points to the exit")]
    public GameObject mapArrow; // Doug
    [Tooltip("The amount of time to show the arrow for when collecting the map powerup")]
    public float arrowTime = 5f;
    
    private GameObject player;
    
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        GetComponents();
        DontDestroyOnLoad(gameObject);

        lives = 3;
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
            //Application.Quit();
        }
        
        UpdateUI();
        
        Respawn();
        GameOver();
    }

    private void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void GetComponents() // AK
    {
        player = GameObject.FindWithTag("Player");
    }
    private void UpdateUI() // AK
    {
        health = player.GetComponent<BasePlayerBehaviour>().health; // AK
        healthText.text = "Health Remaining: " + health + "%"; // AK

        healthSlider.value = (float)health / 100;
        
        lives = player.GetComponent<BasePlayerBehaviour>().lives; // AK
        livesText.text = "Lives Remaining: " + lives; // AK
    }

    private void Respawn() // AK
    {
        if (playerAlive == false)
        {
            respawnText.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Instantiate(player, )
            }
        }
    }
    
    private void GameOver() // AK
    {
        if (lives <= 0)
        {
            gameOverText.enabled = true;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                lives = 3;
                //Instantiate(player, )
            }
        }
    }

    /// <summary>
    /// Toggles the minimap arrow on and off
    /// </summary>
    public void ToggleMapArrow()
    {
        StartCoroutine(ToggleObjectOnOff(mapArrow, arrowTime));
    }
    
    /// <summary>
    /// Toggles a given object on for a set time, then off
    /// </summary>
    /// <param name="toToggle">The object to toggle activity</param>
    /// <param name="time">How long to keep the object active for</param>
    /// <returns>Waits for time seconds before reentering coroutine</returns>
    private IEnumerator ToggleObjectOnOff(GameObject toToggle, float time)
    {
        Debug.Log("setting object visible");
        toToggle.SetActive(true);
        yield return new WaitForSeconds(time);
        Debug.Log("setting object invisible");
        toToggle.SetActive(false);
    }
    
}