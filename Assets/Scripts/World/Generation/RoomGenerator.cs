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
using System.Linq;
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

    [Tooltip("The player prefab that will be spawned")]
    public GameObject player;
    
    [Header("Room Spawn Settings")]
    [Tooltip("How many times to run the room spawning loop")]
    public int spawnRuns = 3;
    
    [Tooltip("The maximum number that can be generated when checking to spawn a room")]
    public float spawnThresholdMax = 10;

    [Tooltip("The minimum number generated that will spawn a room")]
    public float spawnThreshold = 5;

    [Tooltip("Where the player spawns from")]
    public Vector3 playerSpawnpoint;
    
    /// <summary>
    /// Holds a list of all spawned rooms and walls
    /// </summary>
    public List<GameObject> spawnedRooms = new List<GameObject>();
    
    [Tooltip("Is the generator done spawning rooms")]
    public bool spawnDone = false;

    [Tooltip("The arrow that points towards the end")]
    public GameObject roomArrow;

    [Tooltip("The canvas that covers the screen when generating a level")]
    public GameObject loadingCanvas;
    
    /// <summary>
    /// Spawns the rooms on the map
    /// <\summary>
    void Awake()
    {
        rooms = GetComponent<RoomHolder>();
        player = Instantiate(player, playerSpawnpoint, Quaternion.identity);
        NextLevel.SetLevelsComplete(0);
        SpawnMap();
    }
    
    
    /// <summary>
    /// Spawns all the rooms on a floor
    /// </summary>
    public void SpawnMap()
    {
        loadingCanvas.SetActive(true);
        RemoveAllRooms();
        // Spawns the starting room
        GameObject startRoom = Instantiate(rooms.startRoom);
        spawnedRooms.Add(startRoom);
        player.transform.position = playerSpawnpoint;
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
            case EnumList.RoomDoors.DOOR_WIN:
                toSpawnFrom = rooms.winRoom;
                break;
            default:
                toSpawnFrom = rooms.walls;
                break;
        }

        // Hold the values used in the below loop
        GameObject toSpawn;
        RoomBehaviour toSpawnRB;
        // Makes sure only rooms of the correct difficulty spawn
        do
        {
            // Gets a random room from the correct list
            int roomIndex = Random.Range(0, toSpawnFrom.Length);

            // Gets the component needed to check the difficulty
            toSpawnRB =
                toSpawnFrom[roomIndex].GetComponent<RoomBehaviour>();

        } while ((toSpawnRB.difficulty | difficulty) != difficulty);

        toSpawn = toSpawnRB.gameObject;
        
        // Spawns the room at the position of the spawnpoint
        GameObject room = Instantiate(toSpawn, location, Quaternion.identity);
        spawnedRooms.Add(room);
    }

    /// <summary>
    /// Removes rooms that are overlapping
    /// </summary>
    private void RemoveOverlappingRooms()
    {
        // Each sublist contains rooms with the same x-coordinate
        List<List<GameObject>> orderedByX = new List<List<GameObject>>();

        // Splits the rooms into lists of the same x value
        foreach (var room in spawnedRooms)
        {
            // Has the room been placed in the sorted list
            bool isPlaced = false;
            
            // Check if a row for the x-value already exists
            foreach (var row in orderedByX)
            {
                // If it does, add the current room to the row
                if (room.transform.position.x .Equals(row[0].transform.position.x))
                {
                    row.Add(room);
                    isPlaced = true;
                    break;
                }
            }

            // If it does not, add a new row for the x value
            if (!isPlaced)
            {
                orderedByX.Add(new List<GameObject>());
                orderedByX[orderedByX.Count - 1].Add(room);
            }
        }

        List<GameObject> overlappingRooms = new List<GameObject>();
        // Check the y value of each room against the others in the list
        foreach (var roomList in orderedByX)
        {
            // If there is only 1 room in the list, it is not overlapping with anything
            if (roomList.Count == 1)
            {
                continue;
            }

            // Count through all the rooms in the given x-list starting with the first
            for (int i = 0; i < (roomList.Count - 1); ++i)
            {
                // Only compare to ones ahead in the list - ones behind have been checked already
                for (int j = (i + 1); j < roomList.Count; ++j)
                {
                    // IF they have the same y value, they are overlapping
                    if (roomList[i].transform.position.y.Equals(roomList[j].transform.position.y))
                    {
                        // Add the later spawned room to removal list
                        overlappingRooms.Add(roomList[j]);
                    }
                }
            }
        }

        // Remove all rooms in the removal list
        foreach (var room in overlappingRooms)
        {
            Destroy(room);
        }
    }

    /// <summary>
    /// Removes the current map
    /// </summary>
    private void RemoveAllRooms()
    {
        spawnDone = false;
        // Get all room objects
        List<GameObject> allSpawnpoints = GetAllSpawnpoints();
        GameObject[] allPowerups = GameObject.FindGameObjectsWithTag("Powerup");

        // Remove all rooms
        foreach (var room in spawnedRooms)
        {
            Destroy(room);
        }

        // Remove any extra spawnpoints
        foreach (var spawnpoint in allSpawnpoints)
        {
            Destroy(spawnpoint);
        }
        
        // Remove any unused powerups
        foreach (var powerup in allPowerups)
        {
            Destroy(powerup);
        }
        
        spawnedRooms.Clear();
    }

    /// <summary>
    /// Finds all the spawnpoints in the scene, both room and wall
    /// </summary>
    /// <returns>A list of all spawnpoints</returns>
    private List<GameObject> GetAllSpawnpoints()
    {
        List<GameObject> spawnpoints =
            GameObject.FindGameObjectsWithTag("RoomSpawnpoint").ToList();
        spawnpoints.AddRange(GameObject.FindGameObjectsWithTag("WallSpawnpoint"));
        return spawnpoints;
    }
    
    /// <summary>
    /// Spawns the walls around the outside of the map
    /// </summary>
    private IEnumerator SpawnWalls()
    {
        // Forces the function to be called after the triggers
        yield return 0;
        bool hasSpawnedWin = false;
        // Fills the extra spawnpoints in with solid walls so they are not leading to the void
        List<GameObject> roomSpawns = GetAllSpawnpoints();
        // Keep track of how many walls are going to be spawned
        int wallsToSpawn = roomSpawns.Count;
        float winSpawnChance = wallsToSpawn / 2;
        foreach (var spawnpoint in roomSpawns)
        {
            Vector3 spawnLocation = spawnpoint.transform.position;
            EnumList.RoomDoors typeToSpawn = EnumList.RoomDoors.DOOR_NONE;
            // Make sure the win room will be connected by a room
            if (spawnpoint.CompareTag("RoomSpawnpoint") && !hasSpawnedWin && spawnpoint.GetComponent<RoomSpawnSettings>().doorAttachSide != EnumList.RoomDoors.DOOR_NONE)
            {
                // The longer it has gone without spawning the room, the higher the chance
                float spawnChance = Random.Range(winSpawnChance, wallsToSpawn);
                if (spawnChance >= wallsToSpawn - 1)
                {
                    typeToSpawn = EnumList.RoomDoors.DOOR_WIN;
                    hasSpawnedWin = true;
                }
            }

            // Increase the chance of spawning the win room next loop
            ++winSpawnChance;
            SpawnRoom(typeToSpawn, spawnLocation);
        }

        // If the win room did not spawn, force spawn it
        if (!hasSpawnedWin)
        {
            SpawnRoom(EnumList.RoomDoors.DOOR_WIN, roomSpawns[0].transform.position);
        }
        Debug.Log("Spawning Done!");
        roomArrow.SetActive(false);
        loadingCanvas.SetActive(false);
        spawnDone = true;
    }
    
    /// <summary>
    /// Runs the loop that spawns all the walls.
    /// </summary>
    /// <returns>Waits a frame between spawning</returns>
    private IEnumerator SpawnRooms()
    {
        // Controls how many spawn runs there are
        for (int i = 0; i < spawnRuns; ++i)
        {
            // Forces the function to wait a frame between each spawn run
            yield return 0;
            
            // Find all spawnpoings
            GameObject[] roomSpawnpoints =
                GameObject.FindGameObjectsWithTag("RoomSpawnpoint");
            //Debug.Log("Iteration " + i + ", " + (roomSpawnpoints.Length) + " spawnpoints");
            
            // Check if a room should be spawned on each spawnpoint
            foreach (var spawnpoint in roomSpawnpoints)
            {
                RoomSpawnSettings spawnSettings =
                    spawnpoint.GetComponent<RoomSpawnSettings>();
                
                Vector3 spawnLocation = spawnpoint.transform.position;

                
                //DestroyImmediate(spawnpoint.gameObject);
                
                // If the PRNG is not correct, do not spawn on this spawnpoint
                if (Random.Range(0,spawnThresholdMax) < spawnThreshold)
                {
                    continue;
                }

                // Check which door the room needs and spawn it
                foreach (EnumList.RoomDoors door in (EnumList.RoomDoors[]) Enum.GetValues(typeof(EnumList.RoomDoors)))
                {
                    if (CheckDoorAttachSide(spawnSettings, door))
                    {
                        SpawnRoom(door, spawnLocation);
                        break;
                    }
                }
                

            }

            // Clear the array for next iteration
            Array.Clear(roomSpawnpoints, 0, roomSpawnpoints.Length);
            //Debug.Log("End of iteration, " + roomSpawnpoints.Length + " spawnpoints remain");
        }
        
        // Removes rooms that overlap
        RemoveOverlappingRooms();
        
        // Spawn walls once all the rooms have been spawned
        StartCoroutine("SpawnWalls");
    }
}
