using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "RPG/QuestData")]
public class QuestData : ScriptableObject
{
    public string questTitle;
    public string description;
    public CharacterData targetCharacter;
    public string[] requiredItems;
    public string[] optionalDialogueInfo;
    public bool requiresExtraDetails;
}
