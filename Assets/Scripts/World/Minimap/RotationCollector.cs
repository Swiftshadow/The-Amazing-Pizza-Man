using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCollector : MonoBehaviour
{
    /// <summary>
    /// The object to copy the rotation of
    /// </summary>
    public Transform rotationToCopy;

    /// <summary>
    /// Sets the host object's rotation to that of the target
    /// </summary>
    void Update()
    {
        transform.rotation = rotationToCopy.rotation;
    }
}
