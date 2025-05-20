using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public Dialogue dialogue;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void StartDialogue(CharacterData character)
    {
        dialogue.gameObject.SetActive(true);
        dialogue.lines = character.unlockedInteractions;
        dialogue.StartDialogue();
    }
}
