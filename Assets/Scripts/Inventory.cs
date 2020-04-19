using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject Objective;
    public GameObject Walls;
    public TMP_Text ItemDisplay;

    // Start is called before the first frame update
    void Start()
    {
        if (PersistentManager.Instance.level < 2)
        {
            Walls.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        manageItem();
    }
    
    void manageItem()
    {
        if (Input.GetKeyDown("1"))
        {
            ItemDisplay.text = "Item: Objective";
            Walls.SetActive(false);
        }
        else
        {
            if (Input.GetKeyDown("2") && PersistentManager.Instance.level > 1)
            {
                ItemDisplay.text = "Item: Wall";
                Objective.SetActive(false);
            }
        }
    }
}