using System;
using System.ComponentModel;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterMove : MonoBehaviour
{
    public float speed = 1;
    public Transform playerLoc; // gets assigned during spawn
    public int RotationInterval = 30;

    private int elapsedFrames = 0;
    private string direction;
    private Vector3 myPos;
    private bool ov;

    void Update()
    {
        myPos = transform.position;
        if (!PersistentManager.Instance.Running)
        {
            return;
        }

        if (elapsedFrames > RotationInterval)
        {
            SetDirection(playerLoc.position);
            elapsedFrames = 0;
        }

        elapsedFrames++;

        transform.Translate(Vector3.up * (speed * Time.deltaTime));
    }

    public void SetDirection(Vector3 destinationPos)
    {
        if (ov)
        {
            return;
        }
            // // figure out where destination is relative to transform on y axis
        float yDistance = Mathf.Abs(myPos.y - destinationPos.y);
        float xDistance = Mathf.Abs(myPos.x - destinationPos.x);
        
        if (Mathf.Abs(yDistance - xDistance) < speed)
        {
            // this causes the monster to start rotating every other frame, so don't recalculate direction
            return;
        }

        if (yDistance < xDistance)
        {
            // want to set our current direction to the x axis
            setXDirection(destinationPos);
        }
        else
        {
            setYDirection(destinationPos);
        }
    }

    private void setXDirection(Vector3 destPos)
    {
        transform.up = myPos.x < destPos.x ? Vector3.right : Vector3.left;
        direction = myPos.x < destPos.x ? "right" : "left";
    }

    private void setYDirection(Vector3 destPos)
    {
        transform.up = myPos.y < destPos.y ? Vector3.up : Vector3.down;
        direction = myPos.y < destPos.y ? "up" : "down";
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.transform.gameObject.CompareTag("wall"))
        {
            return;
        }

        ov = true;
        elapsedFrames = 0;
        if (direction == "up" || direction == "down")
        {
            setXDirection(playerLoc.position);
        }
        else
        {
            setYDirection((playerLoc.position));
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.transform.gameObject.CompareTag("wall"))
        {
            return;
        }

        ov = false;
    }
}