/*****************************************************************************
// File Name :         PowerupBase.cs
// Author :            Doug Guzman (50%)
                       Andrew Krenzel (50%)
                       
// Creation Date :     4/15/2020
//
// Brief Description : Base class for other powerups to extent. Handles
                       the triggering of the powerup, and the floating
*****************************************************************************/

using UnityEngine;

public class PowerupBase : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerup();
            Destroy(gameObject);
        }
    }


    protected virtual void ApplyPowerup() { }

    // AK IR2
    void Update()
    {
        if (Time.timeScale != 0) {
            Movement();
        }
    }
    
    // AK IR2
    public float frequency = 2.5f;
    public float magnitude = 0.005f;
    
    // AK IR2
    void Movement()
    {
        var pos = transform.position;

        Vector3 sinOffset = Vector3.up * Mathf.Sin(Time.time * frequency);
        sinOffset *= magnitude;
        
        transform.position = pos + sinOffset;
    }
}
