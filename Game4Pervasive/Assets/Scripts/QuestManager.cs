using System.Collections.Generic;
using UnityEngine;
using static QuestData;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public List<QuestData> activeQuests = new();


    // quest obj
    public GameObject questHolder;
    // quest text obj
    public TextMeshProUGUI questText;

    // quest prefab
    public GameObject questPrefab;

    public Transform questListContainer;

    public void Start()
    {
        // for testing we add some quests
        QuestData quest1 = new QuestData
        {
            questId = 1,
            questTitle = "Find the Lost Cat",
            description = "Help the villagers find their lost cat.",
            rewards = new Reward[] { }
        };

        activeQuests.Add(quest1);

        // quest 2
        QuestData quest2 = new QuestData
        {
            questId = 2,
            questTitle = "Collect Herbs",
            description = "Gather herbs from the forest for the healer.",
            rewards = new Reward[] { }
        };

        activeQuests.Add(quest2);

        // update quest text
        UpdateCurrentQuest();
    }


    public void ClearQuestPrefabs()
    {
        // clear all quest prefabs
        foreach (Transform child in questListContainer)
        {
            Destroy(child.gameObject);
        }
    }

    // create quest prefabs
    public void CreateQuestPrefabs()
    {
        foreach (QuestData quest in activeQuests)
        {
            GameObject newQuest = Instantiate(questPrefab, questListContainer);
            Button questButton = newQuest.GetComponent<Button>();
            TextMeshProUGUI buttonText = questButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = quest.questTitle;
        }
    }

    public void CompletedQuest(QuestData quest)
    {
        // remove quest from active quests
        activeQuests.Remove(quest);

        // unlock content based on rewards
        UnlockContent(quest.rewards);

        // update quest book
        ShowQuestBook();

        // update quest text
        UpdateCurrentQuest();
    }

    public void UpdateCurrentQuest()
    {
        // get the first quest of the list
        QuestData firstQuest = activeQuests.Count > 0 ? activeQuests[0] : null;
        if (firstQuest != null)
        {
            questHolder.SetActive(true);
            SetCurrentQuestText(firstQuest);
        }
        else
        {
            questHolder.SetActive(false);
        }
    }

    public void ShowQuestBook()
    {
        ClearQuestPrefabs();
        CreateQuestPrefabs();
    }

    public void SetCurrentQuestText(QuestData quest)
    {
        questText.text = quest.questTitle;
    }

    private void UnlockContent(Reward[] rewards)
    {
        // TODO
    }
}
