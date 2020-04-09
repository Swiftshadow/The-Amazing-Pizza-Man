using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

                newRoomVisible = true;
            }

            //++counter;
        }
    }
}
