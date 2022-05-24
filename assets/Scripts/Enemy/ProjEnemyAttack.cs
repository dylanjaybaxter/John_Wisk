using UnityEngine;

public class ProjEnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform enemyfirePoint;
    [SerializeField] private GameObject[] spits;
    //[SerializeField] private AudioClip spitSFX;

    private Animator anim;
    private ProjEnemyBehavior projEnemyBehavior;
    private float cooldownTimer = Mathf.Infinity; //This allows the enemy to attack immediately

    private void Awake()
    {
        anim = GetComponent<Animator>();
        projEnemyBehavior = GetComponent<ProjEnemyBehavior>();
    }

    private void Update()
    {
        if (cooldownTimer > attackCooldown && projEnemyBehavior.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }
    
    private void Attack()
    {
        //anim.SetTrigger("attack");
        cooldownTimer = 0;

        //GetComponent<AudioSource>().PlayOneShot(spitSFX);
        // object pooling for the projectiles for better performance (Deactivate on hit and reuse when needed)

        spits[FindSpit()].transform.position = enemyfirePoint.position;
        spits[FindSpit()].GetComponent<EnemyProjectile>().SetDirection(Mathf.Sign(-transform.localScale.x)); // Negative sign since default faces left
    }

    //Assisting with pooling
    private int FindSpit()
    {
        for (int i = 0; i < spits.Length; i++)
        {
            if (!spits[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
