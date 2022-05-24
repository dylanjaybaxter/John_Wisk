using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform enemyfirePoint;
    [SerializeField] private GameObject[] webs;

    //private Animator anim;
    private SpiderBehavior SpiderBehavior;
    private float cooldownTimer = Mathf.Infinity;


    // Update is called once per frame
    private void Awake()
    {
        //anim = GetComponent<Animator>();
        SpiderBehavior = GetComponent<SpiderBehavior>();

    }

    private void Update()
    {
        if (cooldownTimer > attackCooldown && SpiderBehavior.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        //anim.SetTrigger("attack");
        cooldownTimer = 0;

        webs[FindWeb()].transform.position = enemyfirePoint.position;
        webs[FindWeb()].GetComponent<WebProjectile>().SetDirection(Mathf.Sign(-transform.localScale.x)); // Negative sign since default faces left
    }

    private int FindWeb()
    {
        for (int i = 0; i < webs.Length; i++)
        {
            if (!webs[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }
    

}
