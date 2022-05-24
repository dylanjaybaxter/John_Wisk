using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebProjectile : MonoBehaviour
{
    [SerializeField] private float proj_speed;
    [SerializeField] private float init_vert_speed;
    private float proj_direction;
    private bool hit;
    private float lifetime = 0;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    //private Animator anim;


    public Transform player;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    private void Update()
    {
        if (hit)
        {
            return;
        }
        float movementSpeed = proj_speed * Time.deltaTime * proj_direction;
        
        if(lifetime == 0)
        {
            rb.AddForce(new Vector2(0, init_vert_speed));
        }
        
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        
        if (lifetime > 3)
        {
            gameObject.SetActive(false);
        }
    }
    
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        proj_direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        // This code changes the direction of the sprite
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //Allows the hairball to disappear 
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        

        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
        Deactivate();
    }
    
}
