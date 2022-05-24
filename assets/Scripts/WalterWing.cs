using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalterWing : StateMachineBehaviour
{

    [SerializeField] private float fly_speed; //Serialize Field allows it to be directly editable in Unity
    [SerializeField] private float fly_ac;
    [SerializeField] private Vector3 fly_Offset;
    [SerializeField] public float detect_range;
    [SerializeField] private float maxHealth;
    // Start is called before the first frame update

    private bool grounded;
    private bool hasSeenPlayer;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private EnemyHealth enemyHealth;
    Transform player;
    Transform walterTransform;
    Rigidbody2D walterRB;
    [SerializeField] float attackRange;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        walterRB = animator.GetComponent<Rigidbody2D>();
        walterTransform = animator.GetComponent<Transform>();
        anim = animator.GetComponent<Animator>();
        boxCollider = animator.GetComponent<BoxCollider2D>();
        enemyHealth = animator.GetComponent<EnemyHealth>();
        hasSeenPlayer = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // attack if player is close enough
        if (Vector2.Distance(player.position, walterRB.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }

        // Start spawn attack every 5 damage
        if ( ((maxHealth - enemyHealth.GetHealth()) % 5 == 0) && enemyHealth.GetHealth() != maxHealth )
        {
            animator.SetTrigger("GoToCenter");
            maxHealth = enemyHealth.GetHealth();
        }

        if (fly_Offset == null)
        {
            fly_Offset = new Vector3(0, 0, 0);
        }
        Vector2 dir = player.position - walterTransform.position + fly_Offset;
        Vector2 dirNorm = dir.normalized;
        if (((dir.magnitude < detect_range) || hasSeenPlayer) && (enemyHealth.GetHealth() > 0))
        {
            walterRB.velocity = walterRB.velocity + fly_ac * dirNorm; // Implements movement
            if (fly_speed < walterRB.velocity.magnitude)
            {
                walterRB.velocity = walterRB.velocity.normalized * fly_speed;
            }
            hasSeenPlayer = true;
        }


        //Section dedicated to flipping the enemy when moving
        if (dir.x > 0.01f)
        {
            walterTransform.localScale = new Vector3(-3, 3, 3);
        }
        else if (dir.x < -0.01f)
        {
            walterTransform.localScale = new Vector3(3, 3, 3);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        walterRB.velocity = new Vector2(0, 0);
    }
}
