using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotationCalculator : MonoBehaviour
{
    private GameObject target;

    private float turnSpeed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("NextLevel");
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindWithTag("NextLevel");
        }

        Vector3 targetPos = target.transform.position;

        Vector2 delta = targetPos - transform.position;
        delta.Normalize();

        // screw turn speed, I have given enough of my time trying to figure it out.
        float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        angle -= 90;

        //Debug.Log("Target angle: " + angle);
        float smooth = Time.deltaTime * turnSpeed;
        //transform.Rotate(new Vector3(0, 0, angle) * smooth);
        Vector3 vecRot = Vector3.RotateTowards(transform.position, new Vector3(transform.position.x, transform.position.y, angle),
            turnSpeed, 360);
        //Quaternion newRot = Quaternion.Euler(0, 0, angle);
        //Debug.Log("Vector z: " + vecRot.z);
        Quaternion newRot = Quaternion.Euler(0, 0, vecRot.z);
        transform.rotation = newRot;
    }
}
