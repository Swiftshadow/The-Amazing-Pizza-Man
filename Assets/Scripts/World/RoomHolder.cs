/*****************************************************************************
// File Name :         RoomHolder.cs
// Author :            Doug Guzman
// Creation Date :     February 17, 2020
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
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

}
