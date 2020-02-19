/*****************************************************************************
// File Name :         RooomSettings.cs
// Author :            Doug Guzman
// Creation Date :     February 19, 2020
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class RoomSpawnSettings : MonoBehaviour
{
    
    public EnumList.RoomDifficulty difficulty;

    public EnumList.RoomDoors doorAttachSide;

    public void AddDoor(EnumList.RoomDoors doorToAdd)
    {
        doorAttachSide = doorAttachSide | doorToAdd;
    }
    
}
