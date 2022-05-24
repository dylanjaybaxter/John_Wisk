using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //Level Variable
    public string LevelName;

    //Load the next level
    public void loadLevel()
    {
        SceneManager.LoadScene(LevelName);
    }

}
