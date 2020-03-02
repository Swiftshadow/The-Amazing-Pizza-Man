/*****************************************************************************
// File Name :         RoomBehaviour.cs
// Author :            Doug Guzman
// Creation Date :     February 19, 2020
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/

using System;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{

    private RoomGenerator generator;

    private void Awake()
    {
        generator = GameObject.FindWithTag("Generator").GetComponent<RoomGenerator>();
        generator.spawnDone = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RoomSpawnpoint"))
        {
            Debug.Log("Destorying " + other.name + " of " + other.transform.parent.name + " in " + gameObject.name);
            Destroy(other.gameObject);
        }
        
        if (gameObject.CompareTag("Wall") && (other.CompareTag("Room") || other.CompareTag("Startpoint")))
        {
            Debug.Log("Destorying wall " + gameObject.name + " in room " + other.transform.name);
            Destroy(gameObject);
        }

        
        if (other.CompareTag("Startpoint") && generator.spawnDone)
        {
            Debug.Log("Destroying " + gameObject.name + " for contacting startpoint");
            Destroy(gameObject);
        }
        
    }
}
