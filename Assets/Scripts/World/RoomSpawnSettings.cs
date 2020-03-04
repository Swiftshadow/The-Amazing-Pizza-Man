/*****************************************************************************
// File Name :         RooomSettings.cs
// Author :            Doug Guzman
// Creation Date :     February 19, 2020
//
// Brief Description : The settings of each individual room
*****************************************************************************/

using UnityEngine;

/// <summary>
/// Lives on spawnpoint objects childed to the main room
/// </summary>
[RequireComponent(typeof(CircleCollider2D))]
public class RoomSpawnSettings : MonoBehaviour
{

    [Tooltip("The side of the room to spawn that must be open")]
    public EnumList.RoomDoors doorAttachSide;

}
