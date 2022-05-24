using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalterWaypoint : StateMachineBehaviour
{
    Transform waypoint; // waypoint to be followed
    Rigidbody2D walterRB;
    [SerializeField] float speed;
    [SerializeField] float acc;
    [SerializeField] float minDist; // min distance to waypoint before spawn attack activates

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        waypoint = GameObject.Find("Center Waypoint").transform;
        walterRB = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(waypoint.position.x, waypoint.position.y);
        Vector2.MoveTowards(walterRB.position, target, speed * Time.deltaTime);

        Vector2 dir = target - walterRB.position;
        Vector2 dirNorm = dir.normalized;
        walterRB.velocity = walterRB.velocity + acc * dirNorm; // Implements movement
        if (speed < walterRB.velocity.magnitude)
        {
            walterRB.velocity = walterRB.velocity.normalized * speed;
        }
        if (Vector2.Distance(target, walterRB.position) <= minDist)
        {
            animator.SetTrigger("Spawn");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Spawn");
        walterRB.velocity = new Vector2(0, 0);
    }
}
