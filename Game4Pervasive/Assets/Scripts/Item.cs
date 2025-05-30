using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    public GameObject interactionPrompt;

    public void Collect()
    {
        Inventory.Instance.AddItem(itemData);
        gameObject.SetActive(false);
    }

    public void PlayerNearby()
    {
        Debug.Log("Player is near the item: " + itemData.itemName);
        interactionPrompt.SetActive(true);
    }

    public void PlayerAway()
    {
        Debug.Log("Player is away from the item: " + itemData.itemName);
        interactionPrompt.SetActive(false);
    }
}
