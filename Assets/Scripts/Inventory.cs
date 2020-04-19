using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject ItemDisplay;
    public Item Objective;
    public Item Wall;
    // public Item ActiveItem;
    public Item[] EnabledItems;

    private Rect boundary;
    private Rect innerBoundary;

    // Start is called before the first frame update
    void Start()
    {
        boundary = GameObject.Find("Scripts").GetComponent<ScreenLayout>().OuterArea;
        innerBoundary = GameObject.Find("Scripts").GetComponent<ScreenLayout>().InnerArea;
        EnabledItems = new[] {Objective};
        if (PersistentManager.Instance.level >= Wall.LevelThreshold)
        {
            EnabledItems = new[] {Objective, Wall};
        }

        // ActiveItem = EnabledItems[0];
    }

    // Update is called once per frame
    void Update()
    {
        // manageItem();
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnItem(Objective);
            updateUI(Objective);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            SpawnItem((Wall));
            updateUI(Wall);
        }
        
    }

    void updateUI(Item item)
    {
        int left = item.MaxQuantity - item.Used;
        item.ItemDisplay.text = String.Format("{0}", left);
        if (left == 0)
        {
            item.ItemDisplay.color = Color.red;
        }
    }

    // void manageItem()
    // {
    //     if (Input.GetKeyDown("1"))
    //     {
    //         ItemDisplay.text = "Item: Objective";
    //         ActiveItem = EnabledItems[0];
    //     }
    //     else
    //     {
    //         if (Input.GetKeyDown("2") && EnabledItems.Length >= 2)
    //         {
    //             ItemDisplay.text = "Item: Wall";
    //             ActiveItem = EnabledItems[1];
    //         }
    //     }
    // }

    public void SpawnItem(Item item)
    {
        if (item.Used >= item.MaxQuantity)
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

        if (item == Objective && innerBoundary.Contains(worldPosition))
        {
            Debug.Log("can't place objective here");
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

        Instantiate(item.ItemInstance, worldPosition, Quaternion.identity);
        item.Used++;
    }
}