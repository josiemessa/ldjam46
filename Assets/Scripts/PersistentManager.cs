using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance { get; private set; }
    public bool Running = true;
    public bool Win;
    public float level = 1;

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

            Running = false;
            Win = false;
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
        Running = false;
        Win = false;
    }

    public void SetLevel(float newLevel)
    {
        level = newLevel;
    }
}