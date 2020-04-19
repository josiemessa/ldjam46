using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance { get; private set; }
    public bool Running = true;
    public bool Win;
    public float level = 0;

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
            SceneManager.sceneLoaded += OnSceneLoaded;

            Running = true;
            Win = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "IntroScene")
        {
            return;
        }
        level++;
        Running = true;
        Win = false;
    }

    public void SetLevel(float newLevel)
    {
        level = newLevel;
    }
}