/*****************************************************************************
// File Name :         EnumList.cs
// Author :            Doug Guzman
// Creation Date :     February 23, 2020
//
// Brief Description : Holds the commonly used enumerations
*****************************************************************************/

using System;

public class EnumList
{
    [Flags]
    public enum RoomDifficulty
    {
        DIFFICULTY_NONE = 0,
        DIFFICULTY_EASY = 1,
        DIFFICULTY_NORMAL = 2,
        DIFFICULTY_HARD = 4
    }

    [Flags]
    public enum RoomDoors
    {
        DOOR_NONE = 0,
        DOOR_LEFT = 1,
        DOOR_RIGHT = 2,
        DOOR_UP = 4,
        DOOR_DOWN = 8
    }
    
}
