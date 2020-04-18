using System;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;
using UnityEngine.AI;

public class MoveToObjective : MonoBehaviour
{
    public float speed = 1;
    public bool arrived;

    private float elapsedTime;

    void Update()
    {
        if (!PersistentManager.Instance.Running)
        {
            return;
        }
        GameObject[] objectives = GameObject.FindGameObjectsWithTag("objective");
        if (objectives.Length == 0 || arrived)
        {
            return;
        }

        Transform destination = objectives[0].transform;
        move(destination);
    }

    // retry will call moveX if cannot move already in line with destination
    void move(Transform destination)
    {
        float d = Mathf.Abs(transform.position.y - destination.position.y);
        if (d != 0)
        {
            // if we're about to go past the destination axis value then just set our axis there.
            if (d < 0.1)
            {
                Debug.Log("setting y axis");
                transform.position = new Vector2(transform.position.x, destination.position.y);
                return;
            }

            Vector2 ytranslation = Vector2.up;
            if (transform.position.y > destination.position.y)
            {
                Debug.Log("down");
                ytranslation = Vector2.down;
            }
            else
            {
                Debug.Log("up");
            }
            
            transform.Translate(ytranslation * (speed * Time.deltaTime));
            return;
        }

        // if we are already in correct y position, move x
        d = Mathf.Abs(transform.position.x - destination.position.x);
        if (d <= 0.1)
        {
            Debug.Log("transforming x axis");
            // if we're about to go past the destination axis value then just set our axis there.
            transform.position = new Vector2(destination.position.x, transform.position.y);
        }
        if (d == 0)
        {
            // we're done but collision should have happened by now anyway...
            Debug.Log("done moving player");
            return;

        }
        Vector2 xtranslation = Vector2.right;
        if (transform.position.x > destination.position.x)
        {
            Debug.Log("Moving left");
            xtranslation = Vector2.left;
        }
        else
        {
            Debug.Log("Moving right");
        }

        transform.Translate(xtranslation * (speed * Time.deltaTime));
        
    }
}