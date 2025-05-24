using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "RPG/QuestData")]
public class QuestData : ScriptableObject
{
    [System.Serializable]
    public struct Reward
    {
        // rewards could be:

        // - a new quest
        // - a new item
        // - approval points

        public int rewardId;
    }
    [System.Serializable]
    public struct QuestNode
    {
        public int questNodeId; // the id of the node
    }

    public string questTitle;
    public string description;
    public int questId; // the id of the quest
    public CharacterDataObj questGiver; // the character that gives the quest
    public QuestNode[] questSequence; // the sequence of nodes that are part of this quest
    public Reward[] rewards; // the rewards that are given by this option
}
