/*****************************************************************************
// File Name :         RoomBehaviour.cs
// Author :            Doug Guzman
// Creation Date :     February 19, 2020
//
// Brief Description : Handles room collisions and spawnpoint deletion
*****************************************************************************/

using System;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{

    private RoomGenerator generator;

    [Tooltip("The difficulty level of this room")]
    public EnumList.RoomDifficulty difficulty;
    
    private void Awake()
    {
        generator = GameObject.FindWithTag("Generator").GetComponent<RoomGenerator>();
        generator.spawnDone = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RoomSpawnpoint") || other.CompareTag("WallSpawnpoint"))
        {
            //Debug.Log("Destorying " + other.name + " of " + other.transform.parent.name + " in " + gameObject.name);
            Destroy(other.gameObject);
        }
        
        if (gameObject.CompareTag("Wall") && (other.CompareTag("Room") || other.CompareTag("Startpoint")))
        {
            Debug.Log("Destorying wall " + gameObject.name + " in room " + other.transform.name);
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("Startpoint") && other.CompareTag("Room") && generator.spawnDone)
        {
            //Debug.Log("Destroying startpoint for being in another room");
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            //Debug.Log("Setting room" + gameObject.name + " visible to minimap");
            gameObject.layer = LayerMask.NameToLayer("MinimapVisible");
            ChangeLayer(gameObject.transform, "MinimapVisible");
        }
    }

    // Adapted from https://answers.unity.com/questions/168084/change-layer-of-child.html
    // New/changes liens are marked with // NEW or // CHANGED
    /// <summary>
    /// Changes the layer of an object and all its children
    /// </summary>
    /// <param name="toChange">The object to change the layer of</param>
    /// <param name="layerToSet">The layer to set the objects to</param>
    public void ChangeLayer(Transform toChange, string layerToSet)
    {
        // Iterated through all children of the current object
        foreach (Transform child in toChange)
        {
            // If the current object is an enemy or plant, skip it
            if (child.gameObject.CompareTag("Enemy") || child.gameObject.CompareTag("Plant")) // NEW
            { // NEW
                continue; // NEW
            } // NEW

            // If the current object is a wall, set it to a special layer still visible to the minimap
            if (child.gameObject.CompareTag("WallMinimapCollider")) // NEW
            { // NEW
                Debug.Log("Setting wall!"); // NEW
                child.gameObject.layer = LayerMask.NameToLayer("WallMinimapVisible"); // NEW
            } // NEW
            else // NEW
            { // NEW
                // Set the layer of the current object
                child.gameObject.layer = LayerMask.NameToLayer("MinimapVisible");
            } // NEW

            // Recursivly change the layer of all the current object's children
            ChangeLayer(child, layerToSet);
        }
    }
}
