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
        interactionPrompt.SetActive(true);
    }

    public void PlayerAway()
    {
        interactionPrompt.SetActive(false);
    }
}
