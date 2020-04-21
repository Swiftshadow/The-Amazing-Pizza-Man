/*****************************************************************************
// File Name :         PotatoBullet.cs
// Author :            Taylor Zarvell 100%
// Creation Date :     3/29/20
//
// Brief Description : Bullet projectile
*****************************************************************************/

using System;
using UnityEngine;

public class PotatoBullet : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;
    private Rigidbody2D rb2d;

    public AudioSource shot;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2d = GetComponent<Rigidbody2D>();
        
        target = new Vector2(player.position.x, player.position.y);

        Vector2 heading = player.position - transform.position;

        Vector2 normalizedDirection = heading / heading.magnitude; 

        rb2d.AddForce(normalizedDirection * speed);

        shot.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);


        if ((transform.position.x == target.x && transform.position.y == target.y))
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        DestroyBullet();
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
