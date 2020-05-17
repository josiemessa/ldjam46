using System;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;
using UnityEngine.AI;

public class MoveToObjective : MonoBehaviour
{
    public float speed = 1;

    // TODO make enum
    private string direction = "up";

    private float elapsedTime;
    private Vector2 destinationPos;

    Vector3 currentEulerAngles;

    void Update()
    {
        if (!PersistentManager.Instance.running)
        {
            return;
        }

        // objective can't be set at 0,0 so this is OK. Don't do equals checks with floating point
        if (Vector2.Distance(destinationPos, Vector2.zero) < 1f)
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
                // look right
                if (direction != "right")
                {
                    Debug.Log("direction is: "+direction+"setting to: right");
                    if (direction == "up")
                    {
                        currentEulerAngles -= new Vector3(0, 0, 90);
                    }
                    else if (direction == "down")
                    {
                        currentEulerAngles += new Vector3(0, 0, 90);
                    }
                    else
                    {
                        currentEulerAngles += new Vector3(0, 0, 180);
                    }
                    direction = "right";
                }
            }
            else
            {
                if (direction != "left")
                {
                    Debug.Log("direction is: "+direction+"setting to: left");
                    if (direction == "up")
                    {
                        currentEulerAngles += new Vector3(0, 0, 90);
                    }
                    else if (direction == "down")
                    {
                        currentEulerAngles -= new Vector3(0, 0, 90);
                    }
                    else
                    {
                        currentEulerAngles += new Vector3(0, 0, 180);
                    }
                    direction = "left";
                }
            }
        }
        else
        {
            if (transform.position.y < destinationPos.y)
            {
                if (direction != "up")
                {
                    Debug.Log("direction is: "+direction+"setting to: up");
                    if (direction == "right")
                    {
                        currentEulerAngles += new Vector3(0, 0, 90);
                    }
                    else if (direction == "left")
                    {
                        currentEulerAngles -= new Vector3(0, 0, 90);
                    }
                    else
                    {
                        currentEulerAngles += new Vector3(0, 0, 180);
                    }
                    direction = "up";
                }
            }
            else
            {
                if (direction != "down")
                {
                    Debug.Log("direction is: "+direction+"setting to: down");
                    if (direction == "right")
                    {
                        currentEulerAngles -= new Vector3(0, 0, 90);
                    }
                    else if (direction == "left")
                    {
                        currentEulerAngles += new Vector3(0, 0, 90);
                    }
                    else
                    {
                        currentEulerAngles += new Vector3(0, 0, 180);
                    }
                    direction = "down";
                }
            }
        }

        //apply the change to the gameObject
        Debug.Log("currentEulerAngles:"+currentEulerAngles);
        transform.eulerAngles = currentEulerAngles;
        transform.Translate(Vector2.up * (speed * Time.deltaTime));
    }
}