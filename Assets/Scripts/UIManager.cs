using System;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    // public UnityEvent onLose;
    public UnityEvent onWin;
    public UnityEvent onLose;
    public TMP_Text LevelDisplay;

    private void Start()
    {
        if (!LevelDisplay)
        {
            Debug.Log("level display not set");
        }

        Debug.Log("level " + PersistentManager.Instance.level);
        LevelDisplay.text = "Level " + PersistentManager.Instance.level;
    }

    private void Update()
    {
        if (PersistentManager.Instance.Win)
        {
            onWin.Invoke();
        }
        else
        {
            if (!PersistentManager.Instance.Running)
            {
                onLose.Invoke();
            }
        }
    }


}