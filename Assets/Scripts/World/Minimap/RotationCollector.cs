/*****************************************************************************
// File Name :         RotationCollector.cs
// Author :            Doug Guzman
                       
// Creation Date :     4/25/2020
//
// Brief Description : Matches the objects rotation to that of the given
                       object.
*****************************************************************************/
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
