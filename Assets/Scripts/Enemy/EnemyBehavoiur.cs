using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehavoiur : MonoBehaviour
{

    public AudioClip death;

    public int health = 10;
    
    public int damageValue;

    private SpriteRenderer rend;

    private bool isInvulnerable = false;

    public GameObject healthDrop;

    public int dropChance = 5;
    
    private void Start()

    {
        rend = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PlayerAttack"))
        {
            if (!isInvulnerable)
            {
                health -= other.GetComponent<AttackBehaviour>().damage;
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

    private void DestroyEnemy()
    {
        int spawnRng = Random.Range(0, 10);

        if (spawnRng <= dropChance)
        {
            Instantiate(healthDrop, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
    
    private IEnumerator FlashColor(Color toFlash)
    {
        Color startColor = rend.color;
        rend.color = toFlash;
        isInvulnerable = true;
        yield return new WaitForSeconds(0.2f);
        rend.color = startColor;
        isInvulnerable = false;
    }
}
