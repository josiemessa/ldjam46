using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(bool reset)
    {
        if (reset)
        {
            if (SceneManager.GetActiveScene().name != "IntroScene")
            {
                PersistentManager.Instance.SetLevel(0);
            }
        }

        // SceneManager.GetActiveScene()
        SceneManager.LoadScene("MainScene");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void EndTutorial()
    {
        PersistentManager.Instance.running = true;
        PersistentManager.Instance.inTutorial = false;
    }
}