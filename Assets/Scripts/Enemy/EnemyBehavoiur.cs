using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavoiur : MonoBehaviour
{

    public int health = 10;
    
    public int damageValue;

    private SpriteRenderer rend;

    private bool isInvulnerable = false;
    
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
            Destroy(gameObject);
        }
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
