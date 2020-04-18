using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjective : MonoBehaviour
{
    public GameObject objective;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Are you clicking on the game screen
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Tile background has collider on it
            RaycastHit2D hitData = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0);
            if (hitData)
            {
                // Clear the z position of the mouse
                worldPosition.z = 0;
                
                // lock objective sprite to grid
                worldPosition.x = Mathf.Floor(worldPosition.x);
                worldPosition.y = Mathf.Floor(worldPosition.y);

                Instantiate(objective, worldPosition, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }
}