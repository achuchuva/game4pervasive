using System.Collections.Generic;
using UnityEngine;
using static QuestData;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public List<QuestData> activeQuests = new();

    public int selectedQuestIndex = 0;


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public List<GameObject> GetAllQuestOptions()
    {
        // just get the children
        var questOptions = new List<GameObject>();
        foreach (Transform child in transform)
        {
            questOptions.Add(child.gameObject);
        }
        return questOptions;
    }

    public GameObject GetSelectedQuestOption()
    {
        if (selectedQuestIndex < 0 || selectedQuestIndex >= transform.childCount)
            return null;

        return transform.GetChild(selectedQuestIndex).gameObject;
    }

    public QuestData GetSelectedQuest()
    {
        if (selectedQuestIndex < 0 || selectedQuestIndex >= activeQuests.Count)
            return null;

        return activeQuests[selectedQuestIndex];
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
