using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float proj_speed;
    private float proj_direction;
    private bool hit;
    private float lifetime;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {

        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }


    private void Update()
    {
        if (hit)
        {
            return;
        }
        float movementSpeed = proj_speed * Time.deltaTime * proj_direction; // Determines which direction it will fly
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 2)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(1);
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

}
