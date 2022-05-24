using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalterAttack : MonoBehaviour
{
    private EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && (enemyHealth.GetHealth() > 0))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
        }
    }
}
