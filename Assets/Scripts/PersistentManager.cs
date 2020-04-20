using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance { get; private set; }
    public bool running = true;
    public bool win;
    public bool inTutorial = true;
    public int level;

    private void OnEnable() 
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            // if (SceneManager.GetActiveScene().name == "IntroScene")
            // {
            //     return;
            // }
            SceneManager.activeSceneChanged += OnSceneChanged;

            running = false;
            win = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneChanged(Scene current, Scene next)
    {
        string name = next.name;
        if (name == "IntroScene")
        {
            return;
        }
        level++;
        running = false;
        win = false;
        inTutorial = true;
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
    }
}