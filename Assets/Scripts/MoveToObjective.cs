using System;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;
using UnityEngine.AI;

public class MoveToObjective : MonoBehaviour
{
    public float speed = 1;
    // TODO make enum
    public string direction;

    private float elapsedTime;

    void Update()
    {
        if (!PersistentManager.Instance.Running)
        {
            return;
        }

        GameObject[] objectives = GameObject.FindGameObjectsWithTag("objective");
        // don't move unless there's an objective.
        if (objectives.Length == 0)
        {
            return;
        }

        GetComponent<Animator>().SetTrigger("Move");

        Transform destination = objectives[0].transform;
        MoveToDestination(destination);
    }

    public void MoveToDestination(Transform destination)
    {
        // figure out where destination is relative to transform on y axis
        float d = Mathf.Abs(transform.position.y - destination.position.y);
        if (d == 0)
        {
            // need to move along the x axis
            d = Mathf.Abs(transform.position.x - destination.position.x);
            if (d == 0)
            {
                // we're done (should have collided but hey)
                return;
            }
        
            // If we're about to move past our destination, just set the x coordinate
            // to the destination
            if (d < 0.1)
            {
                transform.position = new Vector2(destination.position.x, transform.position.y);
                return;
            }
            // Otherwise - turn to look at destination along x axis
            if (transform.position.x < destination.position.x)
            {
                if (direction != "right")
                {
                    direction = "right";
                    transform.up = Vector3.right;
                }
            }
            else
            {
                if (direction != "left")
                {
                    direction = "left";
                    transform.up = Vector3.left;
                }
            }
        
        }
        else
        {
            // Need to move along the y axis
            // if we're about to go past the destination axis value then just set our axis there.
            if (d < 0.1)
            {
                transform.position = new Vector2(transform.position.x, destination.position.y);
                return;
            }

            if (transform.position.y < destination.position.y)
            {
                if (direction != "up")
                {
                    direction = "up";
                    transform.up = Vector3.up;
                }
            }
            else
            {
                if (direction != "down")
                {
                    transform.up = Vector3.down;
                }
            }
        }

        transform.Translate(Vector3.up * (speed * Time.deltaTime));
    }
}