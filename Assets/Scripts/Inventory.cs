using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject ItemDisplay;
    public Item Objective;
    public Item Wall;

    private Rect boundary;
    private Rect innerBoundary;

    // Start is called before the first frame update
    void Start()
    {
        boundary = GameObject.Find("Scripts").GetComponent<ScreenLayout>().OuterArea;
        innerBoundary = GameObject.Find("Scripts").GetComponent<ScreenLayout>().InnerArea;
        if (PersistentManager.Instance.level < 2)
        {
            Wall.ItemDisplay.SetActive(false);
        }
        else
        {
            Wall.ItemDisplay.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PersistentManager.Instance.running)
        {
            return;
        }

        // manageItem();
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnItem(Objective);
            UpdateUI(Objective);
            return;
        }

        if (Input.GetButtonDown("Fire2") && PersistentManager.Instance.level>1)
        {
            SpawnItem((Wall));
            UpdateUI(Wall);
        }
    }

    private static void UpdateUI(Item item)
    {
        var left = item.MaxQuantity - item.Used;
        if (left == 0)
        {
            item.SetText(left.ToString(), Color.red);
        }
        else
        {
            item.SetText(left.ToString());
        }
    }

    private void SpawnItem(Item item)
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