using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBehavior : MonoBehaviour
{
    public bool mustPatrol;
    public bool mustFlip;

    public Transform groundCheckPos;

    private EnemyHealth enemyHealth;
    private float flipCooldown = 0.1f;
    [SerializeField] public float ReactDistance;

    [SerializeField] public float moveSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;


    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private Animator anim;

    public Transform player;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol && enemyHealth.GetHealth() > 0)
        {
            Patrol();
        }

    }

    private void FixedUpdate()
    {
        if (mustPatrol && enemyHealth.GetHealth() > 0)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);

        }
    }

    private void Patrol()
    {
        //Check if end of platform or wall is hit to flip the character
        if (mustFlip || boxCollider.IsTouchingLayers(wallLayer))
        {
            Flip();

        }

        rb.velocity = new Vector2(-1 * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);

    }

    void Flip()
    {

        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        moveSpeed = -1 * moveSpeed;
        mustPatrol = true;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && (enemyHealth.GetHealth() > 0))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
        }
    }

    public bool canAttack()
    {
        if ((Vector2.Distance(transform.position, player.position) < ReactDistance) && (enemyHealth.GetHealth() > 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }



}
