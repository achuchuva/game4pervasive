using UnityEngine;

public class Bubble : MonoBehaviour
{

    public GameObject bubble;

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

    public void ShowBubble()
    {
        bubble.SetActive(true);
        ShowIconsBasedOnData();
    }

    public void HideBubble()
    {
        bubble.SetActive(false);
        HideIcons();
    }

    public void ShowIconsBasedOnData()
    {
        // get character data
        CharacterDataObj characterData = npc.GetCharacterData();

        HideIcons();

        // if ready for TOF show heart
        if (characterData.readyForTOF)
        {
            Debug.Log("Showing heart for character: " + characterData.characterName);
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

    // get sprite renderer
    public SpriteRenderer GetSpriteRenderer()
    {
        return bubble.GetComponent<SpriteRenderer>();
    }
}
