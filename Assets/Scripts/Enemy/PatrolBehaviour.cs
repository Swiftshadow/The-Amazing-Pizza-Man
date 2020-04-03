/*****************************************************************************
// File Name :         PatrolBehaviour.cs
// Author :            Taylor Zarvell 100%
// Creation Date :     3/29/20
//
// Brief Description : Patrol state of monster
*****************************************************************************/
/*using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{

    private PatrolArea patrol;
    public float speed;
    private int randomArea;

    public Transform[] patrolMarkers;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrol = GameObject.FindGameObjectWithTag("PatrolArea").GetComponent<PatrolArea>();
        randomArea = Random.Range(0, patrol.patrolMarkers.Length);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(animator.transform.position, patrol.patrolMarkers[randomArea].position) > 0.2f)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position,
                 patrol.patrolMarkers[randomArea].position, speed * Time.deltaTime);

        }

        else
        {
            randomArea = Random.Range(0, patrol.patrolMarkers.length);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}*/
