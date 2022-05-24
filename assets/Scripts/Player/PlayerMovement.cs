
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float run_speed; //Serialize Field allows it to be directly editable in Unity
    [SerializeField] private float jump_speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform firePoint;

    private bool grounded;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;


    [Header("Crouch Info")]
    public bool canCrouch;

    private Vector2 standingSize;
    private Vector2 standingOffset;
    private Vector2 standingfirePoint;
    public Vector2 crouchingSize;
    public Vector2 crouchingOffset;
    public Vector2 crouchingfirePoint;

    private void Awake() //Runs only when loading the script
    {
        body = GetComponent<Rigidbody2D>(); //Very widely used
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        standingSize = boxCollider.size;
        standingOffset = boxCollider.offset;
        standingfirePoint = firePoint.localPosition;

    }

    private void Update() //Runs on every frame
    {
        Vector3 theScale = transform.localScale;
        theScale.x = Mathf.Abs(theScale.x);
        
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * run_speed, body.velocity.y); // Implements movement

        //Section dedicated to flipping the player when moving
        if(horizontalInput > 0.01f)
        {
            theScale.x *= 1;
            transform.localScale = theScale;
        }

        else if (horizontalInput < -0.01f)
        {
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded()) //Player presses space
        {
            Jump();

        }
        
        
        if(canCrouch && Input.GetKeyDown("s") && isGrounded())
        {
            anim.SetBool("crouch", true);
            boxCollider.size = crouchingSize;
            boxCollider.offset = crouchingOffset;
            firePoint.localPosition = crouchingfirePoint;
        }

        if(canCrouch && (Input.GetKeyUp("s") || !isGrounded()))
        {
            anim.SetBool("crouch", false);
            boxCollider.size = standingSize;
            boxCollider.offset = standingOffset;
            firePoint.localPosition = standingfirePoint;

        }
        
        //Set animator parameters
        anim.SetBool("walking", horizontalInput != 0); //sets boolean walking
        anim.SetBool("grounded", isGrounded());
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jump_speed);
        anim.SetBool("jump", true);
    }


    private bool isGrounded()
    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }


    public bool canAttack()
    {
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Death")
        {
            gameObject.GetComponent<Health>().Fall();
        }
    }

}
