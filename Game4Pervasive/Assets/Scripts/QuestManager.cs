using System.Collections.Generic;
using UnityEngine;
using static QuestData;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public List<QuestData> activeQuests = new();

    public int selectedQuestIndex = 0;

    // quest prefab
    public GameObject questPrefab;

    public GameObject questBook;

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

        // show
        ShowQuestBook();
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
            questButton.onClick.AddListener(() => OnOptionSelected(quest.questId));
        }
    }

    public void ShowQuestBook()
    {
        Debug.Log("Showing quest book");
        questBook.SetActive(true);
        ClearQuestPrefabs();
        CreateQuestPrefabs();


        if (questListContainer.childCount > 0)
        {
            Debug.Log("Setting first button as selected");
            var firstButton = questListContainer.GetChild(0).GetComponent<Button>();

            Debug.Log($"First button: {firstButton.name}");
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstButton.gameObject);

            // select the first quest
            selectedQuestIndex = 0;
        }
    }

    public void HideQuestBook()
    {
        Debug.Log("Hiding quest book");
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
