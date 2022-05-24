using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalterSpawn : MonoBehaviour
{

    [SerializeField] private GameObject enemyType; // spawned enemy fight
    GameObject player; // player so spawned enemies know what to follow
    Transform[] spawnpoints = new Transform[3];
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnpoints[0] = GameObject.Find("SpawnPoint1").transform;
        spawnpoints[1] = GameObject.Find("SpawnPoint2").transform;
        spawnpoints[2] = GameObject.Find("SpawnPoint3").transform;
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < spawnpoints.Length; i++)
        {
            GameObject newEnemy = Instantiate(enemyType, spawnpoints[i]);
            EnemyBehavior enemyBehav = newEnemy.GetComponent<EnemyBehavior>();
            enemyBehav.player = player.transform;
            enemyBehav.attack_range = 20;
        }
    }

}
