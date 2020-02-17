using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{

    public enum TileTypes
    {
        TILE_FLOOR,
        TILE_WALL,
        TILE_ENEMY
    }
    
    
    private int[,] tiles;

    public Room(int width, int height)
    {
        tiles = new int[width, height];
    }

    public int[,] GetTiles()
    {
        return tiles;
    }

    public void SetTile(int row, int column, int type)
    {
        
    }
}
