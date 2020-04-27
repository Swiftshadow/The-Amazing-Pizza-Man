/*****************************************************************************
// File Name :         PotatoGuardBehaviour.cs
// Author :            Taylor Zarvell 100%
// Creation Date :     3/29/20
//
// Brief Description : Basic movement of monster
*****************************************************************************/
using UnityEngine;

public class PotatoGuardBehaviour : MonoBehaviour
{

    public float speed;
    public float stopDistance;
    public float retreatDistance;
    public float trackingDistance = 5f;
    
    public Transform player;

    private float timeBetweenShots;
    public float startTimeBetweenShots;

    public GameObject bullet;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBetweenShots = startTimeBetweenShots;
    }


    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        }
        else if (Vector2.Distance(transform.position, player.position) < stopDistance &&

                 Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }

        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);

        }

        if (timeBetweenShots <= 0 && Vector2.Distance(transform.position, player.transform.position) < 5)
        {

            Instantiate(bullet, transform.position, Quaternion.identity);

            timeBetweenShots = startTimeBetweenShots;

        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
}
