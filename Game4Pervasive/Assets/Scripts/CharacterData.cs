using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "RPG/CharacterData")]
public class CharacterData : ScriptableObject
{
    public struct DialogueTree
    {
        public string optionName;
        public string[] lines;
        public DialogueTree[] branches;
    }

    public struct Conversation
    {
        public DialogueTree[] dialogueTrees;
        public DialogueTree initialDialogueTree;
    }

    public string characterName;
    public Sprite portrait;
    public string backstory;
    public int approvalPoints;
    public int maxApprovalPoints = 10;

    public GameObject[] unlockableDecorations;
    public string[] unlockedInteractions;
}
