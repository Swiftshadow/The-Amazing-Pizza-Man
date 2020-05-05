/*****************************************************************************
// File Name :         GameControllerBehaviour.cs
// Author :            Doug Guzman (80%)
                       Andrew Krenzel (20%)
                       
// Creation Date :     4/15/2020
//
// Brief Description : Base enemy behaviour. Handles enemy health, damage,
                       drop chance, drop items, sounds, and feedback
*****************************************************************************/
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehavoiur : MonoBehaviour
{

    [Tooltip("The sound to play on death")]
    public AudioClip death;

    [Tooltip("How much health the enemy has")]
    public int health = 10;
    
    [Tooltip("How much damage the enemy does")]
    public int damageValue;

    /// <summary>
    /// The object's SpriteRenderer
    /// </summary>
    private SpriteRenderer rend;

    /// <summary>
    /// Is the enemy currently invulnerable
    /// </summary>
    private bool isInvulnerable = false;

    [Tooltip("The object to spawn when an enemy dies")]
    public GameObject enemyDrop;

    [Tooltip("The change to spawn the drop, 0-10")]
    public int dropChance = 5;

    [Tooltip("The particles spawned when the enemy is killed")] //AK IR2
    public GameObject enemyParticles; //AK IR2
    
    /// <summary>
    /// Gets the components needed
    /// </summary>
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }
    
    /// <summary>
    /// If the enemy collides with a player attack, take damage
    /// </summary>
    /// <param name="other">The object collided with</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PlayerAttack"))
        {
            // If the enemy is not invulnerable
            if (!isInvulnerable)
            {
                // Take damage
                health -= other.GetComponent<AttackBehaviour>().damage;
                // Flash red
                StartCoroutine(FlashColor(Color.red));
            }
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            DestroyEnemy();
            AudioSource.PlayClipAtPoint(death, transform.position);
        }
    }

    /// <summary>
    /// Destroy the enemy, and spawn a drop
    /// </summary>
    private void DestroyEnemy()
    {
        // Random number to check if a drop should be spawned
        int spawnRng = Random.Range(0, 10);

        // If the drop should be spawned
        if (spawnRng <= dropChance)
        {
            // Spawn the drop at the enemy's position
            Instantiate(enemyDrop, transform.position, Quaternion.identity);
            Instantiate(enemyParticles, transform.position, Quaternion.identity);
        }

        // Remove the enemy
        Destroy(gameObject);
    }
    
    /// <summary>
    /// Flashes the host a color for 0.2 seconds
    /// </summary>
    /// <param name="toFlash">The color to turn the host</param>
    /// <returns></returns>
    private IEnumerator FlashColor(Color toFlash)
    {
        // Save the starting color to return to it later
        Color startColor = rend.color;
        // Set the color to the given color
        rend.color = toFlash;
        // Make the enemy invulnerable during this period
        isInvulnerable = true;
        // Wait 0.2 seconds
        yield return new WaitForSeconds(0.2f);
        // Return the host to its starting color
        rend.color = startColor;
        // Make the enemy vulnerable aagain
        isInvulnerable = false;
    }
}
