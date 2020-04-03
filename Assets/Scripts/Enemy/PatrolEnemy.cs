/*****************************************************************************
// File Name :         PatrolEnemy.cs
// Author :            Taylor Zarvell 100%
// Creation Date :     4/2/20
//
// Brief Description : Basic Patrolling enemy movement
*****************************************************************************/
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{

    public float speed;
    public float waitTime;
    public float startWaitTime;

    public Transform[] patrolMarkers;
    private int randomMarker;

    void Start()
    {
        waitTime = startWaitTime;
        randomMarker = Random.Range(0, patrolMarkers.Length);
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



    }
}
