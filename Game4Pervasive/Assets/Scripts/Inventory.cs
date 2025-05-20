// Singleton: Inventory.cs
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    private HashSet<string> items = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void AddItem(string item)
    {
        items.Add(item);
    }

    public bool HasItem(string item)
    {
        return items.Contains(item);
    }
}
