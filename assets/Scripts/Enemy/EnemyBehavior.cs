using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float fly_speed; //Serialize Field allows it to be directly editable in Unity
    [SerializeField] private float fly_ac;
    [SerializeField] private Vector3 fly_Offset;
    [SerializeField] public float attack_range;
    // Start is called before the first frame update

    private bool grounded;
    private bool hasSeenPlayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    public Transform player; //Input player
    private EnemyHealth enemyHealth;

    private void Awake() //Runs only when loading the script
    {
        body = GetComponent<Rigidbody2D>(); //Very widely used
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        enemyHealth = GetComponent<EnemyHealth>();
        hasSeenPlayer = false;

    }

    // Update is called once per frame
    private void Update() //Runs on every frame
    {
        if (fly_Offset == null)
        {
            fly_Offset = new Vector3(0, 0, 0);
        }
        Vector2 dir = player.position - transform.position + fly_Offset;
        Vector2 dirNorm = dir.normalized;
        if (((dir.magnitude < attack_range) || hasSeenPlayer) && (enemyHealth.GetHealth() > 0))
        {
            body.velocity = body.velocity + fly_ac * dirNorm; // Implements movement
            if (fly_speed < body.velocity.magnitude)
            {
                body.velocity = body.velocity.normalized * fly_speed;
            }
            hasSeenPlayer = true;
        }


        //Section dedicated to flipping the enemy when moving
        if (dir.x > 0.01f)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }
        else if (dir.x < -0.01f)
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && (enemyHealth.GetHealth() > 0))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
        }
    }

}
