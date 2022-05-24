using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAfterTime : MonoBehaviour
{
    
    [SerializeField] private float loadDelay = 10f;
     [SerializeField] private string sceneName;

     private float timeElapsed;

    // Update is called once per frame
    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > loadDelay){
            SceneManager.LoadScene(sceneName);
        }
    }
}
