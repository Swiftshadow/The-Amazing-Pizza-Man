using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPowerup : PowerupBase
{
    protected override void ApplyPowerup()
    {
        RoomGenerator generator = GameObject.FindWithTag("Generator").GetComponent<RoomGenerator>();

        bool newRoomVisible = false;
        //int counter = 0;
        
        while (!newRoomVisible)
        {

            //newRoomVisible = (counter >= generator.spawnedRooms.Count);
            
            int indexToShow = Random.Range(0, generator.spawnedRooms.Count);

            if (generator.spawnedRooms[indexToShow].layer != 12 && !generator.spawnedRooms[indexToShow].CompareTag("Wall"))
            {
                generator.spawnedRooms[indexToShow].GetComponent<RoomBehaviour>()
                    .ChangeLayer(generator.spawnedRooms[indexToShow].transform, "MinimapVisible");

                Debug.Log("Setting room " + generator.spawnedRooms[indexToShow].name);
                
                newRoomVisible = true;
            }

            //++counter;
        }
    }

    // AK IR2
    void Update()
    {
        Movement();
    }
    
    // AK IR2
    public float frequency = 2.5f;
    public float magnitude = 0.005f;
    
    // AK IR2
    void Movement()
    {
        var pos = transform.position;

        Vector3 sinOffset = Vector3.up * Mathf.Sin(Time.time * frequency);
        sinOffset *= magnitude;

        transform.position = pos + sinOffset;
    }
}
