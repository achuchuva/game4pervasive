using System.Collections.Generic;
using UnityEngine;
using static QuestData;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public List<QuestData> activeQuests = new();

    public int selectedQuestIndex = 0;

    // quest prefab
    public GameObject questPrefab;

    public GameObject questBook;

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

        // show
        ShowQuestBook();
    }

    public void ClearQuestPrefabs()
    {
        // clear all quest prefabs
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    // create quest prefabs
    public void CreateQuestPrefabs()
    {
        foreach (QuestData quest in activeQuests)
        {
            GameObject newQuest = Instantiate(questPrefab, transform);
            Button questButton = newQuest.GetComponent<Button>();
            TextMeshProUGUI buttonText = questButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = quest.questTitle;
            questButton.onClick.AddListener(() => OnOptionSelected(quest.questId));
        }
    }

    public void ShowQuestBook()
    {
        questBook.SetActive(true);
        ClearQuestPrefabs();
        CreateQuestPrefabs();

        // get options container
        Transform optionsContainer = gameObject.transform;

        if (optionsContainer.childCount > 0)
        {
            var firstButton = optionsContainer.GetChild(0).GetComponent<Button>();
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        }
    }

    public void HideQuestBook()
    {
        questBook.SetActive(false);
    }

    private void UnlockContent(Reward[] rewards)
    {
        // TODO
    }

    public void OnOptionSelected(int optionId)
    {
        // set selected quest index
        selectedQuestIndex = activeQuests.FindIndex(q => q.questId == optionId);


        // handle option selection
        Debug.Log($"Option {optionId} selected for quest {activeQuests[selectedQuestIndex].questTitle}");
    }
}
