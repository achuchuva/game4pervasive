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

    private QuestData currentQuest;

    public void Start()
    {
        // for testing we add some quests
        QuestData quest1 = new QuestData
        {
            questId = 1,
            questTitle = "Befriend Tia",
            description = "Help the villagers find their lost cat.",
            rewards = new Reward[] { }
        };

        activeQuests.Add(quest1);

        // quest 2
        QuestData quest2 = new QuestData
        {
            questId = 2,
            questTitle = "Befriend Filbert",
            description = "Gather herbs from the forest for the healer.",
            rewards = new Reward[] { }
        };

        activeQuests.Add(quest2);

        // set the first quest as current
        currentQuest = activeQuests.Count > 0 ? activeQuests[0] : null;

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

            // add listener to the button
            questButton.onClick.AddListener(() => SetCurrentQuest(quest));

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

        // get the first quest of the list
        currentQuest = activeQuests.Count > 0 ? activeQuests[0] : null;

        // update quest text
        UpdateCurrentQuest();
    }

    public void UpdateCurrentQuest()
    {
        if (currentQuest != null)
        {
            questHolder.SetActive(true);
            SetCurrentQuestText(currentQuest);
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

    public void SetCurrentQuest(QuestData quest)
    {
        currentQuest = quest;
        UpdateCurrentQuest();
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
