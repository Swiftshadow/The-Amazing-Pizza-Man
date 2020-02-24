/*****************************************************************************
// File Name :         RooomBehaviour.cs
// Author :            Doug Guzman
// Creation Date :     February 19, 2020
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/

using System;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    /*
    private void Awake()
    {
        gameObject.name = "Room " + RoomGenerator.roomSpawnCount;
        RaycastHit2D hit =
            Physics2D.Linecast(transform.position, transform.position);
        Debug.Log(gameObject.name + " Checking for spawnpoint");
        if (hit.collider != null)
        {
            Debug.Log(gameObject.name + " linecast hit " + hit.transform.name);
            if (hit.transform.CompareTag("RoomSpawnpoint"))
            {
                Debug.Log("Destroying spawnpoint");
                Destroy(hit.transform.gameObject);
            }
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RoomSpawnpoint"))
        {
            Debug.Log("Destorying " + other.name + " in trigger");
            Destroy(other.gameObject);
        }
    }
}
