using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MapPowerup : PowerupBase
{
    /// <summary>
    /// Reference to the game controller
    /// </summary>
    private GameControllerBehaviour gc;

    /// <summary>
    /// Sets the reference to the game controller
    /// </summary>
    private void Awake()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<GameControllerBehaviour>();
    }

    /// <summary>
    /// Sets the ability of the powerup. Method overrides parent class, which runs it on trigger enter.
    /// </summary>
    protected override void ApplyPowerup()
    {
        // Finds the room generator
        RoomGenerator generator = GameObject.FindWithTag("Generator").GetComponent<RoomGenerator>();

        // Loop through all spawned rooms. Map powerup will show them in order spawned
        for(int i = 0; i < generator.spawnedRooms.Count; ++i)
        {
            // If the selected room is not visible to the minimap, and is not a wall, make it visible to the minimap
            if (generator.spawnedRooms[i].layer != 12 && !generator.spawnedRooms[i].CompareTag("Wall"))
            {
                generator.spawnedRooms[i].GetComponent<RoomBehaviour>()
                    .ChangeLayer(generator.spawnedRooms[i].transform, "MinimapVisible");

                Debug.Log("Setting room " + generator.spawnedRooms[i].name);
            }
        }
        
        // Tell the game controller to toggle the minimap arrow
        gc.ToggleMapArrow();
    }

    // AK IR2
    void Update()
    {
        Movement();
    }
    
    // AK IR2
    public float frequency = 2.5f;
    public float magnitude = 0.005f;
    
    // AK IR2
    void Movement()
    {
        var pos = transform.position;

        Vector3 sinOffset = Vector3.up * Mathf.Sin(Time.time * frequency);
        sinOffset *= magnitude;

        transform.position = pos + sinOffset;
    }
}
