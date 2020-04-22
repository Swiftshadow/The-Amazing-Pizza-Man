using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCollector : MonoBehaviour
{
    public Transform rotationToCopy;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = rotationToCopy.rotation;
    }
}
