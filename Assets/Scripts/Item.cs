using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int MaxQuantity;
    public int Used;
    public int LevelThreshold;
    public GameObject ItemInstance;
    public GameObject ItemDisplay;

    private Text ItemText;

    private void Start()
    {
        ItemText = ItemDisplay.GetComponentInChildren<Text>();
        SetText(MaxQuantity.ToString());
    }

    public void SetText(string t)
    {
        ItemText.text = t;
    }
    public void SetText(string t, Color c)
    {
        ItemText.text = t;
        if (c != null)
        {
            ItemText.color = c;
        }
    }
}