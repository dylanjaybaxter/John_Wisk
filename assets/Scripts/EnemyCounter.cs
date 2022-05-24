using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemyNumText;
    [SerializeField] int enemyNum;
    public GameObject levelLoader;
    private void Awake() //Runs only when loading the script
    {
        enemyNumText.text = "Targets Remaining: " + enemyNum;
        levelLoader = GameObject.Find("LevelLoader");
    }
    public void oneLessEnemy()
    {
        enemyNum -= 1;
        enemyNumText.text = "Targets Remaining: " + enemyNum;
        if(enemyNum <= 0)
        {
            levelLoader.GetComponent<LevelLoader>().LoadNextLevel();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}