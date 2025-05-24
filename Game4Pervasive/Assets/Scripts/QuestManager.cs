using System.Collections.Generic;
using UnityEngine;
using static QuestData;

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
        if (!metExtraDetails) return;

        var charData = quest.questGiver;
        if (charData.approvalPoints < charData.maxApprovalPoints)
        {
            charData.approvalPoints++;
            UnlockContent(quest.rewards);
        }
    }

    private void UnlockContent(Reward[] rewards)
    {
        // TODO
    }
}
