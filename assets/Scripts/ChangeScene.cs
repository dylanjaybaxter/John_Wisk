using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void restartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void startGame()
    {
        SceneManager.LoadScene("Cutscene 1");
    }

    public void goToStory()
    {
        SceneManager.LoadScene("StoryScreen");
    }

    public void goToControls()
    {
        SceneManager.LoadScene("ControlScreen");
    }

    public void goToCredits()
    {
        SceneManager.LoadScene("CreditsScreen");
    }

    public void retryLevel()
    {
        SceneManager.LoadScene(StoreLevel.nameOfCurrentLevel);
    }
}
