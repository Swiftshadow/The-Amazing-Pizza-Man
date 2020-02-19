/*****************************************************************************
// File Name :         RoomGenerator.cs
// Author :            Doug Guzman
// Creation Date :     February 12, 2020
//
// Brief Description : Generates the layout for a specific room of the level
*****************************************************************************/

using System;
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
        Instantiate(rooms.startRoom);

        GameObject[] roomSpawnpoints =
            GameObject.FindGameObjectsWithTag("RoomSpawnpoint");

        foreach (var spawnpoint in roomSpawnpoints)
        {
            RoomSpawnSettings spawnSettings =
                spawnpoint.GetComponent<RoomSpawnSettings>();

            if (CheckDoorAttachSide(spawnSettings, EnumList.RoomDoors.DOOR_RIGHT))
            {
                          
            }
        }
    }

    private bool CheckDoorAttachSide(RoomSpawnSettings settings, EnumList.RoomDoors toCheck)
    {
        return ((settings.doorAttachSide & EnumList.RoomDoors.DOOR_RIGHT) != 0);
    }

    private void SpawnRoom(EnumList.RoomDoors attachSide)
    {
        GameObject roomToSpawn;
        GameObject[] toSpawnFrom;
        switch(attachSide)
        {
            case EnumList.RoomDoors.DOOR_UP:
                toSpawnFrom = rooms.topRooms;
                break;
            case EnumList.RoomDoors.DOOR_DOWN:
                toSpawnFrom = rooms.bottomRooms;
                break;
            case EnumList.RoomDoors.DOOR_LEFT:
                toSpawnFrom = rooms.leftRooms;
                break;
            case EnumList.RoomDoors.DOOR_RIGHT:
                toSpawnFrom = rooms.rightRooms;
                break;
        }
        
        // Pick random room from the list and spawn it
        
    }
}
