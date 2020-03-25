/*****************************************************************************
// File Name :         BroccoliMonster.cs
// Author :            Taylor Zarvell 100%
// Creation Date :     3/2/20
//
// Brief Description : Basic movement of monster 
*****************************************************************************/
using UnityEngine;

public class BroccoliMonster : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 3f;


    // get the rigidbody
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform; // AK
    }

    // find position
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        }
        
        Vector3 direction = player.position - transform.position;

        direction.Normalize();
        movement = direction; 
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
