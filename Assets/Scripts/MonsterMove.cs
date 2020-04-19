using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterMove : MonoBehaviour
{
    public float speed = 1;
    public Transform playerLoc; // gets assigned during spawn

    void Update()
    {
        if (!PersistentManager.Instance.Running)
        {
            return;
        }

        MoveToDestination(playerLoc);
    }


    public void MoveToDestination(Transform destination)
    {
        // figure out where destination is relative to transform on y axis
        float yDistance = Mathf.Abs(transform.position.y - destination.position.y);
        float xDistance = Mathf.Abs(transform.position.x - destination.position.x);

        if (xDistance > yDistance)
        {
            // Look at destination along x axis
            if (transform.position.x < destination.position.x)
            {
                Debug.Log("Rotating to look right");
                transform.up = Vector3.right;
            }
            else
            {
                Debug.Log("Rotating to look left");
                transform.up = Vector3.left;
            }
        }
        else
        {
            if (transform.position.y < destination.position.y)
            {
                Debug.Log("Rotating to look up");
                transform.up = Vector3.up;
            }
            else
            {
                Debug.Log("Rotating to look down");
                transform.up = Vector3.down;
            }
        }

        transform.Translate(Vector3.up * (speed * Time.deltaTime));
    }
}