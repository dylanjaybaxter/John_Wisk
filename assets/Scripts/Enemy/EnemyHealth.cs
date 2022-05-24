using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    EnemyCounter enemyCount;
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth;
    private Animator anim;
    private bool dead;
    private Rigidbody2D body;


    private void Awake()
    {
        currentHealth = startingHealth;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dead = false;
        enemyCount = GameObject.FindGameObjectWithTag("Counter").GetComponent<EnemyCounter>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        //Fly is dead
        if (currentHealth <= 0)
        {
            if(!dead)
            {
                enemyCount.oneLessEnemy();
            }
            dead = true;
            anim.SetTrigger("die");
            body.velocity = new Vector2(0, 0);

            Destroy(this.gameObject, 0.5f);
        }

    }
    
    public float GetHealth()
    {
        return currentHealth;
    }


}
