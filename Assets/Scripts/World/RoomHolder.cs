/*****************************************************************************
// File Name :         RoomHolder.cs
// Author :            Doug Guzman
// Creation Date :     February 17, 2020
//
// Brief Description : Holds the room prefabs to be spawned
*****************************************************************************/
using UnityEngine;

public class RoomHolder : MonoBehaviour
{
    
    [Tooltip("Holds all rooms with a door on the top")]
    public GameObject[] topRooms;

    [Tooltip("Holds all rooms with a door on the bottom")]
    public GameObject[] bottomRooms;
    
    [Tooltip("Holds all rooms with a door on the left")]
    public GameObject[] leftRooms;
    
    [Tooltip("Holds all rooms with a door on the right")]
    public GameObject[] rightRooms;

    [Tooltip("The starting room for a floor")]
    public GameObject startRoom;

    [Tooltip("A room with no doors. Used to block extra exits")]
    public GameObject[] walls;

    [Tooltip("Room that will win the current level")]
    public GameObject[] winRoom;

}
