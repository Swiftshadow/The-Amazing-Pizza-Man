/*****************************************************************************
// File Name :         EnumList.cs
// Author :            Doug Guzman
// Creation Date :     February 12, 2020
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/

using System;

public class EnumList
{
    public enum RoomDifficulty
    {
        DIFFICULTY_EASY,
        DIFFICULTY_MEDIUM,
        DIFFICULTY_HARD
    }

    /// <summary>
    /// Set up to be bitwise - each one has its own flags for if the door is there or not
    /// </summary>
    [Flags]
    public enum RoomDoors
    {
        DOOR_NONE = 0,
        DOOR_UP = 1,
        DOOR_DOWN = 2,
        DOOR_LEFT = 4,
        DOOR_RIGHT = 8
    }
}
