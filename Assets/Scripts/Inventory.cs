using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public TMP_Text ItemDisplay;
    public Item Objective;
    public Item Wall;
    public Item ActiveItem;
    public Item[] EnabledItems;

    private Rect boundary;

    // Start is called before the first frame update
    void Start()
    {
        boundary = GameObject.Find("Scripts").GetComponent<ScreenLayout>().OuterArea;
        EnabledItems = new[] {Objective};
        if (PersistentManager.Instance.level >= Wall.LevelThreshold)
        {
            EnabledItems = new[] {Objective, Wall};
        }

        ActiveItem = EnabledItems[0];
    }

    // Update is called once per frame
    void Update()
    {
        manageItem();
        SpawnItem();
    }

    void manageItem()
    {
        if (Input.GetKeyDown("1"))
        {
            ItemDisplay.text = "Item: Objective";
            ActiveItem = EnabledItems[0];
        }
        else
        {
            if (Input.GetKeyDown("2") && EnabledItems.Length >= 2)
            {
                ItemDisplay.text = "Item: Wall";
                ActiveItem = EnabledItems[1];
            }
        }
    }

    public void SpawnItem()
    {
        if (ActiveItem.Used >= ActiveItem.MaxQuantity)
        {
            return;
        }

        if (!Input.GetButtonDown("Fire1"))
        {
            return;
        }

        // Are you clicking on the game screen
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!boundary.Contains(worldPosition))
        {
            Debug.Log("out of bounds click");
            return;
        }

        // don't add an item if we have an object here
        RaycastHit2D hitData = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0);
        if (hitData)
        {
            Debug.Log("object already in this spot, ignoring click");
            return;
        }

        // Clear the z position of the mouse
        worldPosition.z = 0;

        // lock objective sprite to grid
        worldPosition.x = Mathf.Floor(worldPosition.x);
        worldPosition.y = Mathf.Floor(worldPosition.y);

        Instantiate(ActiveItem.ItemInstance, worldPosition, Quaternion.identity);
        ActiveItem.Used++;
    }
}