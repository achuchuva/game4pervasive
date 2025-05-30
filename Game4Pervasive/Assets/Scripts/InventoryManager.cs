using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Inventory inventory;

    // inventory container
    public GameObject inventoryContainer;

    // inventory item prefab
    public GameObject inventoryItemPrefab;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClearInventoryItems()
    {
        // clear all inventory items
        foreach (Transform child in inventoryContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void CreateInventoryItems()
    {
        // create inventory items
        foreach (ItemData item in inventory.GetAllItems())
        {
            GameObject newItem = Instantiate(inventoryItemPrefab, inventoryContainer.transform);

            // text
            TextMeshProUGUI itemText = newItem.GetComponentInChildren<TextMeshProUGUI>();
            itemText.text = item.itemName;

            // image
            Image itemImage = newItem.GetComponentInChildren<Image>();
            itemImage.sprite = item.image;
        }
    }

    public void ShowInventory()
    {
        // show inventory
        inventoryContainer.SetActive(true);
        ClearInventoryItems();
        CreateInventoryItems();
    }
}
