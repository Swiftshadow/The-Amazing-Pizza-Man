using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPowerup : PowerupBase
{
    public float arrowTime = 5f;
    private GameObject arrow;

    private void Awake()
    {
        arrow = GameObject.FindWithTag("MapArrow");
    }
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
        
        StartCoroutine(ToggleObjectOnOff(arrow, arrowTime));
    }

    private IEnumerator ToggleObjectOnOff(GameObject toToggle, float time)
    {
        Debug.Log("setting object visible");
        toToggle.SetActive(true);
        yield return new WaitForSeconds(time);
        Debug.Log("setting object invisible");
        toToggle.SetActive(false);
    }
}
