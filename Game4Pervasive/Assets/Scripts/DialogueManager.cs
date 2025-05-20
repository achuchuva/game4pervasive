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
        dialogue.image.sprite = character.portrait;
        isDialogueActive = true;
        dialogue.StartDialogue();
    }

    public void FindItem(ItemData item)
    {
        dialogue.gameObject.SetActive(true);
        dialogue.lines = new[] { "You found " + item.itemName + "!\n" + item.description };
        dialogue.image.sprite = item.image;
        isDialogueActive = true;
        dialogue.StartDialogue();
    }
}
