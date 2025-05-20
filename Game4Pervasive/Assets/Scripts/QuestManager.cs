using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public List<QuestData> activeQuests = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void StartQuest(QuestData quest)
    {
        if (!activeQuests.Contains(quest))
            activeQuests.Add(quest);
    }

    public void CompleteQuest(QuestData quest, bool metExtraDetails)
    {
        activeQuests.Remove(quest);
        if (!metExtraDetails && quest.requiresExtraDetails) return;

        var charData = quest.targetCharacter;
        if (charData.approvalPoints < charData.maxApprovalPoints)
        {
            charData.approvalPoints++;
            UnlockContent(charData);
        }
    }

    private void UnlockContent(CharacterData charData)
    {
        foreach (var item in charData.unlockableDecorations)
            item.SetActive(true); // or Instantiate it somewhere

        // Handle unlocked interactions similarly
    }
}
