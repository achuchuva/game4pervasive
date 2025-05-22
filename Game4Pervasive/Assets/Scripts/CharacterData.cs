using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "RPG/CharacterData")]
public class CharacterData : ScriptableObject
{
    [System.Serializable]
    public struct DialogueTree
    {
        public string optionName;
        public string[] lines;
        public DialogueTree[] branches;
    }

    [System.Serializable]
    public struct Conversation
    {
        public DialogueTree[] dialogueTrees;
    }

    public string characterName;
    public Sprite portrait;
    public string backstory;
    public int approvalPoints;
    public int maxApprovalPoints = 10;

    public Conversation[] conversations;
    public GameObject[] unlockableDecorations;
    public string[] unlockedInteractions;

}
