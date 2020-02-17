/*****************************************************************************
// File Name :         RoomGenerator.cs
// Author :            Doug Guzman
// Creation Date :     February 12, 2020
//
// Brief Description : Generates the layout for a specific room of the level
*****************************************************************************/

using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{

    public EnumList.RoomDifficulty difficulty;

    private RoomHolder rooms;
    
    /// <summary>
    /// Generates a room array and places the tiles for said room
    /// <\summary>
    void Awake()
    {
        rooms = GameObject.FindWithTag("RoomHolder").GetComponent<RoomHolder>();
        SpawnRooms();
    }


    private void SpawnRooms()
    {
        
    }
}
