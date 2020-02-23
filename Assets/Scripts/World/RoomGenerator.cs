/*****************************************************************************
// File Name :         RoomGenerator.cs
// Author :            Doug Guzman
// Creation Date :     February 12, 2020
//
// Brief Description : Generates the room layout for the current level
*****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomGenerator : MonoBehaviour
{

    [Tooltip("What difficulty of rooms to spawn")]
    public EnumList.RoomDifficulty difficulty;

    /// <summary>
    /// Holds all the room prebafs
    /// </summary>
    private RoomHolder rooms;

    [Tooltip("How many times to run the room spawning loop")]
    public int spawnRuns = 3;

    [Tooltip("The maximum number that can be generated when checking to spawn a room")]
    public float spawnThresholdMax = 10;

    [Tooltip("The lowest number generated that will spawn a room")]
    public float spawnThreshold = 5;
    
    /// <summary>
    /// Generates a room array and places the tiles for said room
    /// <\summary>
    void Awake()
    {
        rooms = GetComponent<RoomHolder>();
        SpawnRooms();
    }

    public GameObject[] roomSpawnpoints;
    
    /// <summary>
    /// Spawns all the rooms on a floor
    /// </summary>
    private void SpawnRooms()
    {
        // Spawns the starting room
        Instantiate(rooms.startRoom);

        for (int i = 0; i < spawnRuns; ++i) {
            roomSpawnpoints =
                GameObject.FindGameObjectsWithTag("RoomSpawnpoint");
            Debug.Log("Iteration " + i + ", " + (roomSpawnpoints.Length) + " spawnpoints");
            foreach (var spawnpoint in roomSpawnpoints)
            {
                RoomSpawnSettings spawnSettings =
                    spawnpoint.GetComponent<RoomSpawnSettings>();

                Vector3 spawnLocation = spawnpoint.transform.position;
                
                Destroy(spawnpoint.gameObject);
                
                if (Random.Range(0,spawnThresholdMax) < spawnThreshold)
                {
                    Debug.Log("Not spawning room from " + spawnpoint.name + " on " + spawnpoint.transform.parent.name);
                    continue;
                }

                if (CheckDoorAttachSide(spawnSettings, EnumList.RoomDoors.DOOR_RIGHT))
                {
                    Debug.Log("Spawning right from " + spawnpoint.name + " on " + spawnpoint.transform.parent.name);
                    SpawnRoom(EnumList.RoomDoors.DOOR_RIGHT, spawnLocation);
                }
                else if (CheckDoorAttachSide(spawnSettings, EnumList.RoomDoors.DOOR_LEFT))
                {
                    Debug.Log("Spawning left from " + spawnpoint.name + " on " + spawnpoint.transform.parent.name);
                    SpawnRoom(EnumList.RoomDoors.DOOR_LEFT, spawnLocation);
                }
                else if (CheckDoorAttachSide(spawnSettings, EnumList.RoomDoors.DOOR_UP))
                {
                    Debug.Log("Spawning up from " + spawnpoint.name + " on " + spawnpoint.transform.parent.name);
                    SpawnRoom(EnumList.RoomDoors.DOOR_UP, spawnLocation);
                }
                else if (CheckDoorAttachSide(spawnSettings, EnumList.RoomDoors.DOOR_DOWN))
                {
                    Debug.Log("Spawning down from " + spawnpoint.name + " on " + spawnpoint.transform.parent.name);
                    SpawnRoom(EnumList.RoomDoors.DOOR_DOWN, spawnLocation);
                }
                
            }

            //Array.Clear(roomSpawnpoints, 0, roomSpawnpoints.Length);
            Debug.Log("End of iteration, " + roomSpawnpoints.Length + " spawnpoints remain");
        }
        
        /*
        // Fills the extra spawnpoints in with solid walls so they are not leading to the void
        GameObject[] roomSpawns =
            GameObject.FindGameObjectsWithTag("RoomSpawnpoint");
        foreach (var spawnpoint in roomSpawns)
        {
            Vector3 spawnLocation = spawnpoint.transform.position;
            SpawnRoom(EnumList.RoomDoors.DOOR_NONE, spawnLocation);
        }
        */
    }

    /// <summary>
    /// Checks what side needs to be open for the doors to line up
    /// </summary>
    /// <param name="settings">The settings of the room to be checked</param>
    /// <param name="toCheck">The side to check if it is valid</param>
    /// <returns></returns>
    private bool CheckDoorAttachSide(RoomSpawnSettings settings, EnumList.RoomDoors toCheck)
    {
        return ((settings.doorAttachSide & toCheck) != 0);
    }

    /// <summary>
    /// Spawns an individual room
    /// </summary>
    /// <param name="attachSide">The side that must be open to attach the room</param>
    /// <param name="location">Where on the map to spawn the room</param>
    private void SpawnRoom(EnumList.RoomDoors attachSide, Vector3 location)
    {
        // Stores the array to spawn rooms from
        GameObject[] toSpawnFrom;
        // Checks what side needs to be open
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
            default:
                toSpawnFrom = rooms.walls;
                break;
        }

        // Gets a random room from the correct list
        int roomIndex = Random.Range(0, toSpawnFrom.Length);

        // Spawns the room at the position of the spawnpoint
        GameObject room = Instantiate(toSpawnFrom[roomIndex], location, Quaternion.identity);
    }
}
