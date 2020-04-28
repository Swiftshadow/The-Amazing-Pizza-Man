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
        Destroy(gameObject, 0.1f);
    }
    
}
