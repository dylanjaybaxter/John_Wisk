using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] hairballs;
    [SerializeField] private AudioClip hairballSFX;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity; //This allows the player to attack immediately

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }
    
    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        GetComponent<AudioSource>().PlayOneShot(hairballSFX);
        // object pooling for the projectiles for better performance (Deactivate on hit and reuse when needed)

        hairballs[FindHairball()].transform.position = firePoint.position;
        hairballs[FindHairball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    //Assisting with pooling
    private int FindHairball()
    {
        for (int i= 0; i < hairballs.Length; i++)
        {
            if (!hairballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
