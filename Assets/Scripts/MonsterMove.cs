using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterMove : MonoBehaviour
{
    public float speed = 1;
    public Transform playerLoc; // gets assigned during spawn

    private bool yLock = false;

    private void Awake()
    {
        // pick whether to move horizontally or vertically
        if (Random.value < 0.5)
        {
            yLock = true;
        }
    }

    void Update()
    {
        if (!PersistentManager.Instance.Running)
        {
            return;
        }
        if (yLock)
        {
            moveY(true);
        }
        else
        {
            moveX(true);
        }
    }


    // retry will call moveX if cannot move already in line with playerLoc
    void moveY(bool retry)
    {
        if (transform.position.y != playerLoc.position.y)
        {
            // if we're about to go past the playerLoc axis value then just set our axis there.
            float d = Mathf.Abs(transform.position.y - playerLoc.position.y);
            if (d < 0.1)
            {
                transform.position = new Vector2(transform.position.x,playerLoc.position.y);
                return;
            }

            
            Vector2 translation = Vector2.up;
            if (transform.position.y > playerLoc.position.y)
            {
                translation = Vector2.down;
            }
            transform.Translate(translation * (speed * Time.deltaTime));
        }
        else
        {
            if (retry)
            {
                yLock = false;
                moveX(false);
            }
        }
    }

    // retry will call moveY if cannot move already in line with playerLoc
    void moveX(bool retry)
    {
        if (transform.position.x != playerLoc.position.x)
        {
            // if we're about to go past the playerLoc axis value then just set our axis there.
            float d = Mathf.Abs(transform.position.x - playerLoc.position.x);
            if (d < 0.1)
            {
                transform.position = new Vector2(playerLoc.position.x, transform.position.y);
                return;
            }
            
            Vector2 translation = Vector2.right;
            if (transform.position.x > playerLoc.position.x)
            {
                translation = Vector2.left;
            }

            transform.Translate(translation * (speed * Time.deltaTime));
        }
        else
        {
            if (retry)
            {
                yLock = true;
                moveY(false);
            }
        }
    }
}