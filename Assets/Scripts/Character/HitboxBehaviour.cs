/*****************************************************************************
// File Name :         HitboxBehaviour.cs
// Author :            Doug Guzman
                       
// Creation Date :     4/15/2020
//
// Brief Description : Destroys the hitbox after a set amount of time
*****************************************************************************/
using UnityEngine;

public class HitboxBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Remove", 0.2f);
    }

    private void Remove()
    {
        Destroy(gameObject);
    }
}
