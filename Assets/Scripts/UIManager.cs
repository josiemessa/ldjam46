using System;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // public UnityEvent onLose;    
    public TMP_Text LevelDisplay;
    public GameObject ItemDisplay;
    public GameObject Tutorial;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public AudioClip gameMusic;
    public AudioClip fanfareMuic;
    public GameObject musicSource;

    private bool gameStarted;
    private int level;
    private PersistentManager pm;
    private SceneLoader sl;
    private AudioSource audioSrc;

    private void Start()
    {
        pm = PersistentManager.Instance;
        sl = gameObject.GetComponent<SceneLoader>();
        level = pm.level;
        audioSrc = musicSource.GetComponent<AudioSource>();

        Debug.Log("level" + level);
        if (pm.inTutorial)
        {
            if (level < 3)
            {
                Tutorial.SetActive(true);
                TMP_Text ttext = Tutorial.GetComponentInChildren<TMP_Text>();
                switch (level)
                {
                    case 1:
                        ttext.text = FirstTutorialText;
                        break;
                    case 2:
                        ttext.text = SecondTutorialText;
                        break;
                }

                if (level > 1)
                {
                    audioSrc.clip = fanfareMuic;
                    audioSrc.Play();
                }
            }
            else
            {
                Tutorial.SetActive(false);
                pm.inTutorial = false;
                pm.running = true;
            }
        }
    }

    private void Update()
    {
        // Don't display any UI elements when we're in the tutorial
        if (pm.inTutorial)
        {
            return;
        }

        // If we're out of the tutorial and the game is running, display UI elements
        if (pm.running)
        {
            ItemDisplay.SetActive(true);
            LevelDisplay.gameObject.SetActive(true);;
            if (LevelDisplay.text == "")
            {
                LevelDisplay.text = "Level " + level;
            }


            if (audioSrc.clip != gameMusic)
            {
                Debug.Log("Setting audio clip");
                audioSrc.clip = gameMusic;
                audioSrc.Play();
            }

            return;
        }

        // If we're not running and not in the tutorial, the game is over,
        // display win/lose screens
        if (!pm.win)
        {
            LoseScreen.SetActive(true);
        }
        else
        {
            if (level > 1)
            {
                if (audioSrc.clip != (fanfareMuic))
                {
                    audioSrc.clip = fanfareMuic;
                    audioSrc.Play();
                }

                WinScreen.SetActive(true);
            }
            else
            {
                sl.LoadScene(false);
            }
        }
    }

    private const string FirstTutorialText = @"Player 1 Has Entered The Game

Unfortunately your player isn't the smartest. Lucky for you, an objective is ready for use, so your player will know where to go. 
You can place the objective on the map by clicking your LEFT MOUSE BUTTON anywhere on the GREEN OUTER BORDER. 
You only have 1 objective, so choose its location wisely.";

    private const string SecondTutorialText = @"Objective Found!

Despite all the odds, your player made it to the objective!
But level 2 is next, which means things need to get harder. Thankfully, walls are now ready for use. Place a wall anywhere on the map by clicking RIGHT MOUSE BUTTON.
You can see how many walls you have left at the top of the screen.
And try to avoid the monsters, they bite.
Good luck and godspeed.";
}