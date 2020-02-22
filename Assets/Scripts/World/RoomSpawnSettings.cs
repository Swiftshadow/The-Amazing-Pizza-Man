/*****************************************************************************
// File Name :         RooomSettings.cs
// Author :            Doug Guzman
// Creation Date :     February 19, 2020
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

/// <summary>
/// Lives on spawnpoint objects childed to the main room
/// </summary>
public class RoomSpawnSettings : MonoBehaviour
{
    
    [Tooltip("Difficulty of the room to spawn")]
    public EnumList.RoomDifficulty difficulty;

    [Tooltip("The side of the room to spawn that must be open")]
    public EnumList.RoomDoors doorAttachSide;

    /// <summary>
    /// Adds a door to the list of must open
    /// </summary>
    /// <param name="doorToAdd">The side that must be open</param>
    public void AddDoor(EnumList.RoomDoors doorToAdd)
    {
        doorAttachSide = doorAttachSide | doorToAdd;
    }
    
}
