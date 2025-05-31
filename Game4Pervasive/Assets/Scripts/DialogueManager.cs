using System.Collections.Generic;
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

    public Conversation GetCurrentConversation(Conversation[] conversations)
    {
        // TODO implement this
        return conversations[0];
    }

    public void StartDialogue(CharacterDataObj character)
    {
        dialogue.gameObject.SetActive(true);

        Debug.Log("Starting dialogue with " + character.characterName);

        Conversation currentConversation = GetCurrentConversation(character.conversations);

        dialogue.titleImage.gameObject.SetActive(true);
        dialogue.titleText.text = character.characterName;
        isDialogueActive = true;
        if (character.readyForTOF)
        {
            dialogue.TOFCharacter = character.characterName;
        }
        else
        {
            dialogue.TOFCharacter = string.Empty; // no TOF character for non-TOF dialogues
        }
        dialogue.StartDialogue(currentConversation.conversationTree);
    }

    public void FindItem(ItemData item)
    {
        dialogue.gameObject.SetActive(true);
        dialogue.titleImage.gameObject.SetActive(false);
        isDialogueActive = true;

        // create dummy conversation

        // TODO kinda hacky
        int ITEM_CONVERSATION_ID = 999;

        // no branches
        DialogueNode[] branches = new DialogueNode[0];

        // number of lines
        int numLines = string.IsNullOrEmpty(item.description) ? 1 : 2;

        // text
        string[] lines = new string[numLines];

        // name
        // if plural use "some" otherwise use "a"
        lines[0] = (item.isPlural ? "You found some " : "You found a ") + item.itemName;

        // don't show if description is empty
        if (numLines > 1)
        {
            lines[1] = item.description;
        }

        DialogueNode tree = new DialogueNode(ITEM_CONVERSATION_ID, "Item Found", lines, branches);

        dialogue.TOFCharacter = string.Empty; // no TOF character for items
        dialogue.StartDialogue(tree);
    }
}
