/*****************************************************************************
// File Name :         PotatoBullet.cs
// Author :            Taylor Zarvell 100%
// Creation Date :     3/29/20
//
// Brief Description : Bullet projectile
*****************************************************************************/
using UnityEngine;

public class PotatoBullet : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);


        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyBullet();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
