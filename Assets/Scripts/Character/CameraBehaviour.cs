/*****************************************************************************
// File Name :         CameraBehaviour.cs
// Author :            Doug Guzman
// Creation Date :     March 04, 2020
//
// Brief Description : Controls the camera movement around the scene
*****************************************************************************/
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    /// <summary>
    /// Reference to the player
    /// </summary>
    private GameObject player;

    [Tooltip("How high above the player the camera should hover")]
    public float hoverDistance = -10;
    
    /// <summary>
    /// Finds and sets the player reference
    /// <\summary>
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    /// <summary>
    /// Moves the camera to be over the player at all times
    /// <\summary>
    void Update()
    {
        try
        {
            Vector3 targetPos = player.transform.position;

            targetPos.z = hoverDistance;

            transform.position = targetPos;
        }
        catch (MissingReferenceException e)
        {
            Debug.Log("Player missing, trying to refind");
            player = GameObject.FindWithTag("Player");
        }
    }
}
