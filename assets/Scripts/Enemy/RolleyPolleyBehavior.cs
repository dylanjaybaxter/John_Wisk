using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolleyPolleyBehavior : MonoBehaviour
{
    public bool mustPatrol;
    public bool chargeState;
    public bool prepareCharge;
    public bool mustFlip;
    private bool isFacingLeft = true;

    public Transform groundCheckPos;
    public Transform castPoint;

    private EnemyHealth enemyHealth;
    private float flipCooldown = 0.1f;
    [SerializeField] public float ReactDistance;
    [SerializeField] public float waitTime;

    [SerializeField] public float moveSpeed;
    [SerializeField] public float chargeSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;


    private float cooldownTimer = Mathf.Infinity;

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
        if(enemyHealth.GetHealth() > 0)
        {
            if (mustPatrol && enemyHealth.GetHealth() > 0)
            {
                Patrol();
            }
            else if (prepareCharge && !mustPatrol && enemyHealth.GetHealth() > 0)
            {
                PrepareCharge();
            }
            else if (chargeState && !mustPatrol && enemyHealth.GetHealth() > 0)
            {
                Charge();
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
        

    }

    private void FixedUpdate()
    {
        if ((chargeState || mustPatrol) && enemyHealth.GetHealth() > 0)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);

        }
    }

    private void Patrol()
    {
        //Check if end of platform or wall is hit to flip the character
        if (mustFlip || boxCollider.IsTouchingLayers(wallLayer))
        {
            if (cooldownTimer > flipCooldown)
            {
                Flip();
            }
                
        }

        rb.velocity = new Vector2(-1 * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);

        if (detectPlayer())
        {
            mustPatrol = false;
            prepareCharge = true;
            chargeState = false;
        }

        cooldownTimer += Time.deltaTime;
    }

    private void PrepareCharge()
    {
        rb.velocity = new Vector2(0, rb.velocity.y); ;
        anim.SetBool("notice", true);
        Invoke("Charge", waitTime);

    }


    private void Charge()
    {
        if (!chargeState)
        {
            anim.SetBool("notice", false);
            chargeState = true;
            prepareCharge = false;
        }
        else
        {
            anim.SetBool("roll", true);
            rb.velocity = new Vector2(-1 * chargeSpeed * Time.fixedDeltaTime, rb.velocity.y);

            //If it hits a wall or edge stop charging and patrol
            if (mustFlip || boxCollider.IsTouchingLayers(wallLayer))
            {
                anim.SetBool("roll", false);
                if (cooldownTimer > flipCooldown)
                {
                    Flip();
                }    
                chargeState = false;
            }
        }

        if(!chargeState)
        {
            //stop charging and enter patrol mode
            chargeState = false;
            mustPatrol = true;
        }
        cooldownTimer += Time.deltaTime;
    }

    void Flip()
    {
        
        cooldownTimer = 0;
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        moveSpeed = -1 * moveSpeed;
        chargeSpeed = -1 * chargeSpeed;
        isFacingLeft = !isFacingLeft;
        mustPatrol = true;
        
    }

    bool detectPlayer()
    {
        //Create a raycast that looks for the player to charge
        bool val = false;
        var castDist = ReactDistance;
        
        if (!isFacingLeft) //facing right
        {
            castDist = -ReactDistance;
        }

        Vector2 endPos = castPoint.position + Vector3.left * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));



        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }

            Debug.DrawLine(castPoint.position, endPos, Color.yellow);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }

        
        return val;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && (enemyHealth.GetHealth() > 0))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
        }
    }



}
