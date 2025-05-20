using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "RPG/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite image;
    public string description;
}
