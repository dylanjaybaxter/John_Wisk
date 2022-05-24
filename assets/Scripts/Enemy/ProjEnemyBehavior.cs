using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjEnemyBehavior : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    [SerializeField] public float ReactDistance;

    [SerializeField] public float moveSpeed;
    [SerializeField] public Transform[] waypoints;

    public Transform player;

    int waypointIndex = 0;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        transform.position = waypoints[waypointIndex].transform.position;
        enemyHealth = GetComponent<EnemyHealth>();
    }
    
    void Update()
    {
        if (enemyHealth.GetHealth() > 0)
        {
            Move();
        }

    }

    public bool canAttack()
    {
        if ((Vector2.Distance(transform.position, player.position) < ReactDistance) && (enemyHealth.GetHealth() > 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Move()
    {
        Vector3 currentPosition = this.transform.position;

        Vector3 targetPosition = waypoints[waypointIndex].transform.position;

        if(Vector3.Distance(currentPosition, targetPosition) > 0.1f)
        {
            Vector3 directionOfTravel = targetPosition - currentPosition;
            directionOfTravel.Normalize();


            this.transform.Translate(directionOfTravel.x * moveSpeed * Time.deltaTime, directionOfTravel.y * moveSpeed * Time.deltaTime, 0, Space.World);

        }
        else
        {
            NextWaypoint();
        }
        
    }

    private void NextWaypoint()
    {
        waypointIndex = (waypointIndex + 1 >= waypoints.Length) ? 0 : waypointIndex + 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && (enemyHealth.GetHealth() > 0))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
        }
    }

}
