using UnityEngine;

public class Bubble : MonoBehaviour
{

    public NPC npc;
    public GameObject heart;
    public GameObject questIcon;

    void ShowHeart()
    {
        heart.SetActive(true);
    }

    void ShowQuest()
    {
        questIcon.SetActive(true);
    }

    void HideIcons()
    {
        // hide both icons
        heart.SetActive(false);
        questIcon.SetActive(false);
    }

    public void ShowIconsBasedOnData()
    {
        // get character data
        CharacterDataObj characterData = npc.GetCharacterData();

        HideIcons();

        // if ready for TOF show heart
        if (characterData.readyForTOF)
        {
            ShowHeart();
        }
        else
        {
            // if they have a quest
            bool hasQuest = characterData.availableQuests.Length > 0;

            if (hasQuest)
            {
                ShowQuest();
            }
        }
    }
}
