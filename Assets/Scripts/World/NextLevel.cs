using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{

    private RoomGenerator generator;
    // Start is called before the first frame update
    void Start()
    {
        generator = GameObject.FindWithTag("Generator").GetComponent<RoomGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            generator.difficulty = (EnumList.RoomDifficulty) ((int) generator.difficulty << 1);
            generator.SpawnMap();
        }
    }
}
