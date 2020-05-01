/*****************************************************************************
// File Name :         ShootingEnemyBehaviour.cs
// Author :            Taylor Zarvell 100%
// Creation Date :     5/28/20
//
// Brief Description : Patrolling and shooting enemy
*****************************************************************************/
using UnityEngine;

public class ShootingEnemyBehaviour : MonoBehaviour
{

    public float speed;
    public float waitTime;
    public float startWaitTime;

    public Transform[] patrolMarkers;
    private int randomMarker;



    public float bulletSpeed;
    public float stopDistance;
    public float retreatDistance;
    public float trackingDistance = 5f;

    public Transform player;

    private float timeBetweenShots;
    public float startTimeBetweenShots;

    public GameObject bullet;


    void Start()
    {
        waitTime = startWaitTime;
        randomMarker = Random.Range(0, patrolMarkers.Length);


        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBetweenShots = startTimeBetweenShots;
    }


    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolMarkers[randomMarker].position,
           speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolMarkers[randomMarker].position) < .2f)
        {
            if (waitTime <= 0)
            {

                randomMarker = Random.Range(0, patrolMarkers.Length);
                waitTime = startWaitTime;
            }

            else
            {
                waitTime -= Time.deltaTime;
            }
        }



        if (Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, bulletSpeed * Time.deltaTime);

        }
        else if (Vector2.Distance(transform.position, player.position) < stopDistance &&

                 Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }

        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -bulletSpeed * Time.deltaTime);

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
