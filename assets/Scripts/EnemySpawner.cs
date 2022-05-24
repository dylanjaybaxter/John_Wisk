using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 1f; // time between spawns
    [SerializeField] private GameObject enemyType = new GameObject(); // array containing enemy game objects
    [SerializeField] private GameObject player; // player so spawned enemies know what to follow
    [SerializeField] private Transform[] spawnpoints = new Transform[4];

    [SerializeField] private int numOfEnemiesLeft; // how many enemies left in wave
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(spawnInterval, enemyType));
    }

    // Update is called once per frame
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        if(numOfEnemiesLeft > 0)
        {
            Transform enemySP = spawnpoints[Mathf.RoundToInt(Random.Range(0, spawnpoints.Length - 1))]; // Return random spawn point 
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, enemySP);
            EnemyBehavior enemyBehav = newEnemy.GetComponent<EnemyBehavior>();
            enemyBehav.player = player.transform;
            enemyBehav.attack_range = 20;
            StartCoroutine(spawnEnemy(interval, enemy));
            numOfEnemiesLeft--;
        }
    }
}
