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
    private Vector2 destinationPos;

    void Update()
    {
        if (!PersistentManager.Instance.running)
        {
            return;
        }

        // objective can't be set at 0,0 so this is OK. Don't do equals checks with floating point
        if (Vector2.Distance(destinationPos, Vector2.zero)<1f)
        {
            // Only get the objective position once
            GameObject[] objectives = GameObject.FindGameObjectsWithTag("objective");
            // don't move unless there's an objective.
            if (objectives.Length == 0)
            {
                return;
            }

            destinationPos = objectives[0].transform.position;
            GetComponent<Animator>().SetTrigger("Move");
        }

        MoveToDestination();
    }
    

    public void MoveToDestination()
    {
        // figure out where destination is relative to transform on y axis
        float d = Mathf.Abs(transform.position.y - destinationPos.y);
        if (d < 0.1f)
        {
            // need to move along the x axis
            d = Mathf.Abs(transform.position.x - destinationPos.x);
            if (d < 0.1f)
            {
                // we're done (should have collided but hey)
                return;
            }

            // Otherwise - turn to look at destination along x axis
            if (transform.position.x < destinationPos.x)
            {
                if (direction != "right")
                {
                    direction = "right";
                    transform.up = Vector2.right;
                }
            }
            else
            {
                if (direction != "left")
                {
                    direction = "left";
                    transform.up = Vector2.left;
                }
            }
        }
        else
        {
            if (transform.position.y < destinationPos.y)
            {
                if (direction != "up")
                {
                    direction = "up";
                    transform.up = Vector2.up;
                }
            }
            else
            {
                if (direction != "down")
                {
                    transform.up = Vector2.down;
                }
            }
        }

        transform.Translate(Vector2.up * (speed * Time.deltaTime));
    }
}