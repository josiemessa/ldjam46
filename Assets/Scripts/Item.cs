using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int MaxQuantity;
    public int Used;
    public int LevelThreshold;
    public GameObject ItemInstance;
    public Text ItemDisplay;

}