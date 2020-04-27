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
        
        // We have not made a random room visible yet
        bool newRoomVisible = false;

        // While a new room is not visible
        while (!newRoomVisible)
        {
            // Generate a random number up to the number of spawned rooms
            int indexToShow = Random.Range(0, generator.spawnedRooms.Count);

            // If the selected room is not visible to the minimap, and is not a wall, make it visible to the minimap
            if (generator.spawnedRooms[indexToShow].layer != 12 && !generator.spawnedRooms[indexToShow].CompareTag("Wall"))
            {
                generator.spawnedRooms[indexToShow].GetComponent<RoomBehaviour>()
                    .ChangeLayer(generator.spawnedRooms[indexToShow].transform, "MinimapVisible");

                Debug.Log("Setting room " + generator.spawnedRooms[indexToShow].name);
                
                // We have made a room visible, exit the loop
                newRoomVisible = true;
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
