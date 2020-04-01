using System.Collections;
using System.Collections.Generic;
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
