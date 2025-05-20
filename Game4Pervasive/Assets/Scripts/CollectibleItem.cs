using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemName;

    public void Collect()
    {
        Inventory.Instance.AddItem(itemName);
        gameObject.SetActive(false);
    }
}
