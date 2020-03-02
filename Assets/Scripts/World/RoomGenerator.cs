/*****************************************************************************
// File Name :         RoomGenerator.cs
// Author :            Doug Guzman
// Creation Date :     February 12, 2020
//
// Brief Description : Generates the room layout for the current level
*****************************************************************************/

using System;
using System.Collections;
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

    public bool spawnDone = false;
    
    /// <summary>
    /// Generates a room array and places the tiles for said room
    /// <\summary>
    void Awake()
    {
        rooms = GetComponent<RoomHolder>();
        SpawnMap();
    }
    
    
    /// <summary>
    /// Spawns all the rooms on a floor
    /// </summary>
    private void SpawnMap()
    {
        // Spawns the starting room
        Instantiate(rooms.startRoom);
        spawnDone = false;
        StartCoroutine("SpawnRooms");
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

    /// <summary>
    /// Removes the current map
    /// </summary>
    void RemoveRooms()
    {
        spawnDone = false;
        GameObject[] allRooms = GameObject.FindGameObjectsWithTag("Room");
        GameObject[] allSpawnpoints = GameObject.FindGameObjectsWithTag("RoomSpawnpoint");
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject startpoint = GameObject.FindWithTag("Startpoint");
        
        Destroy(startpoint);
        foreach (var room in allRooms)
        {
            DestroyImmediate(room);
        }

        foreach (var spawnpoint in allSpawnpoints)
        {
            DestroyImmediate(spawnpoint);
        }

        foreach (var wall in walls)
        {
            Destroy(wall);
        }
    }

    /// <summary>
    /// Spawns the walls around the outside of the map
    /// </summary>
    private IEnumerator SpawnWalls()
    {
        // Forces the function to be called after the triggers
        yield return 0;
        // Fills the extra spawnpoints in with solid walls so they are not leading to the void
        GameObject[] roomSpawns =
            GameObject.FindGameObjectsWithTag("RoomSpawnpoint");
        foreach (var spawnpoint in roomSpawns)
        {
            Vector3 spawnLocation = spawnpoint.transform.position;
            SpawnRoom(EnumList.RoomDoors.DOOR_NONE, spawnLocation);
        }
        
        Debug.Log("Spawning Done!");
        spawnDone = true;
    }
    
    /// <summary>
    /// Runs the loop that spawns all the walls.
    /// </summary>
    /// <returns>Waits a frame between spawning</returns>
    private IEnumerator SpawnRooms()
    {
        for (int i = 0; i < spawnRuns; ++i)
        {
            yield return 0;
            GameObject[] roomSpawnpoints =
                GameObject.FindGameObjectsWithTag("RoomSpawnpoint");
            Debug.Log("Iteration " + i + ", " + (roomSpawnpoints.Length) + " spawnpoints");
            
            
            foreach (var spawnpoint in roomSpawnpoints)
            {
                RoomSpawnSettings spawnSettings =
                    spawnpoint.GetComponent<RoomSpawnSettings>();
                
                Vector3 spawnLocation = spawnpoint.transform.position;

                if (Random.Range(0,spawnThresholdMax) < spawnThreshold)
                {
                    continue;
                }

                if (CheckDoorAttachSide(spawnSettings, EnumList.RoomDoors.DOOR_RIGHT))
                {
                    SpawnRoom(EnumList.RoomDoors.DOOR_RIGHT, spawnLocation);
                }
                else if (CheckDoorAttachSide(spawnSettings, EnumList.RoomDoors.DOOR_LEFT))
                {
                    SpawnRoom(EnumList.RoomDoors.DOOR_LEFT, spawnLocation);
                }
                else if (CheckDoorAttachSide(spawnSettings, EnumList.RoomDoors.DOOR_UP))
                {
                    SpawnRoom(EnumList.RoomDoors.DOOR_UP, spawnLocation);
                }
                else if (CheckDoorAttachSide(spawnSettings, EnumList.RoomDoors.DOOR_DOWN))
                {
                    SpawnRoom(EnumList.RoomDoors.DOOR_DOWN, spawnLocation);
                }

            }

            Array.Clear(roomSpawnpoints, 0, roomSpawnpoints.Length);
            Debug.Log("End of iteration, " + roomSpawnpoints.Length + " spawnpoints remain");
        }
        
        StartCoroutine("SpawnWalls");
    }

    /// <summary>
    /// Can regenerate the map with the R key
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            spawnDone = false;
            RemoveRooms();
            SpawnMap();
        }
    }
}
