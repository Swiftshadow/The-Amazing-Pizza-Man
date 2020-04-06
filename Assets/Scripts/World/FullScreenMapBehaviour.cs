using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenMapBehaviour : MonoBehaviour
{

    public GameObject fullScreenMap;

    private bool toggleShowMap = false;

    private bool showMap = false;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("ToggleMap") > 0)
        {
            if (!toggleShowMap)
            {
                showMap = !showMap;
                toggleShowMap = true;
            }
        }

        if (Input.GetAxis("ToggleMap") == 0)
        {
            toggleShowMap = false;
        }
        
        fullScreenMap.SetActive(showMap);
    }
}
