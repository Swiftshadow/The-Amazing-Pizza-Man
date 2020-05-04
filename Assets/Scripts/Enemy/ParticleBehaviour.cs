/*****************************************************************************
// File Name :         ParticleBehaviour.cs
// Author :            Andrew Krenzel 
// Creation Date :     2/13/2020
//
// Brief Description : Creates and then destroys the particle effect that the
                        script is attached to.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SelfDestruct();
    }
    
    private void SelfDestruct()
    {
        Destroy(gameObject, 0.5f);
    }
    
}
