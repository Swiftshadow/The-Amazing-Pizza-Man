/*****************************************************************************
// File Name :         MapPowerup.cs
// Author :            Doug Guzman
                       
// Creation Date :     4/15/2020
//
// Brief Description : Map powerup class. Turns the minimap arrow on, and
                       shows the rooms on the minimap in spawn order
*****************************************************************************/

using UnityEngine;

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
                break;
            }
        }
        
        // Tell the game controller to toggle the minimap arrow
        gc.ToggleMapArrow();
    }
}
