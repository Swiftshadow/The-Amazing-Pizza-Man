using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotationCalculator : MonoBehaviour
{
    /// <summary>
    /// The target object for rotation
    /// </summary>
    private GameObject target;

    /// <summary>
    /// How fast to rotate - has no visible impact on code.
    /// </summary>
    private float turnSpeed = 1.5f;
    
    /// <summary>
    /// Gets the target object
    /// </summary>
    void Start()
    {
        target = GameObject.FindWithTag("NextLevel");
    }

    /// <summary>
    /// Keeps the rotation giver object pointing towards the exit
    /// </summary>
    void Update()
    {
        // If the target no longer exists, try to find it again
        if (target == null)
        {
            target = GameObject.FindWithTag("NextLevel");
        }

        // Rotation section adapted from the Tower Defense individual assignment
        
        // The target location to rotate to
        Vector3 targetPos = target.transform.position;

        // Get the normalized difference in positions
        Vector2 delta = targetPos - transform.position;
        delta.Normalize();
        
        // Calculate the angle between the target and current rotation
        float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        // Calculation above is off by 90 degrees, rectify that
        angle -= 90;
        
        // Calculates the angle needed to face the target
        Vector3 vecRot = Vector3.RotateTowards(transform.position, new Vector3(transform.position.x, transform.position.y, angle),
            turnSpeed, 360);
        // Creates a quaternion of the correct rotation
        Quaternion newRot = Quaternion.Euler(0, 0, vecRot.z);
        // Sets the rotation of the host object
        transform.rotation = newRot;
    }
}
