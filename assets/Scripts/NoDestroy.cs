using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoDestroy : MonoBehaviour
{

    private Scene scene;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }


    private void Update(){
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if ((musicObj.Length > 1))
        {

            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
