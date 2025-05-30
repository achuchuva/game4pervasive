using UnityEngine;


[System.Serializable]
public class DialogueNode
{
    public int dialogueNodeId;
    public string optionName;
    public string[] lines;

    public DialogueNode[] branches;

    // constructor
    public DialogueNode(int id, string name, string[] lines, DialogueNode[] branches)
    {
        dialogueNodeId = id;
        optionName = name;
        this.lines = lines;
        this.branches = branches;
    }
}

[System.Serializable]
public class Conversation
{
    public int conversationId;
    public string conversationName;
    public DialogueNode conversationTree;

    // constructor
    public Conversation(int id, string name, DialogueNode tree)
    {
        conversationId = id;
        conversationName = name;
        conversationTree = tree;
    }
}


[System.Serializable]
public class CharacterDataObj
{
    public int characterId;
    public string characterName;
    public int approvalPoints;
    public int maxApprovalPoints;
    public bool readyForTOF;
    public QuestData[] availableQuests;
    public Conversation[] conversations;
}


