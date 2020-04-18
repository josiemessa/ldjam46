using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWalls : MonoBehaviour
{
    public GameObject Wall;
    public float Quantity;
    public float placed = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PersistentManager.Instance.level < 2)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (placed >= Quantity)
        {
            return;
        }
        // update in SpawnObjective as well if this changes
        if (Input.GetButtonDown("Fire1"))
        {
            // Are you clicking on the game screen
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Tile background has collider on it
            RaycastHit2D hitData = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0);
            if (hitData)
            {
                // don't add a wall if we have one here already
                if (hitData.transform.gameObject.CompareTag("wall"))
                {
                    return;
                }
                // Clear the z position of the mouse
                worldPosition.z = 0;
                
                // lock objective sprite to grid
                worldPosition.x = Mathf.Floor(worldPosition.x);
                worldPosition.y = Mathf.Floor(worldPosition.y);

                Instantiate(Wall, worldPosition, Quaternion.identity);
                placed++;
            }
        }
    }
}
