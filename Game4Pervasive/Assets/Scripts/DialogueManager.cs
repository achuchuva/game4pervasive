using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public bool isDialogueActive;
    public Dialogue dialogue;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void StartDialogue(CharacterData character)
    {
        dialogue.gameObject.SetActive(true);
        dialogue.lines = character.unlockedInteractions;
        dialogue.titleImage.gameObject.SetActive(true);
        dialogue.titleText.text = character.characterName;
        isDialogueActive = true;
        dialogue.StartDialogue();
    }

    public void FindItem(ItemData item)
    {
        dialogue.gameObject.SetActive(true);
        dialogue.lines = new[] { "You found " + item.itemName + "!\n" + item.description };
        dialogue.titleImage.gameObject.SetActive(false);
        isDialogueActive = true;
        dialogue.StartDialogue();
    }
}
