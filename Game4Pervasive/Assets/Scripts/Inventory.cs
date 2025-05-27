using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    private HashSet<ItemData> items = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(ItemData item)
    {
        items.Add(item);
        DialogueManager.Instance.FindItem(item);
    }

    public bool HasItem(ItemData item)
    {
        return items.Contains(item);
    }
}
