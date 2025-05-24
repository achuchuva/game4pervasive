using UnityEngine;

public class NPC : MonoBehaviour

{
    public CharacterLoader characterLoader;
    public string characterFileName;
    public GameObject interactionPrompt;
    private CharacterDataObj characterData;

    private void Start()
    {
        characterData = characterLoader.LoadCharacter(characterFileName);
    }

    void Update()
    {
        if (DialogueManager.Instance.isDialogueActive)
        {
            interactionPrompt.SetActive(false);
        }
    }

    public void Interact()
    {
        DialogueManager.Instance.StartDialogue(characterData);
    }

    public void PlayerNearby()
    {
        interactionPrompt.SetActive(true);
    }

    public void PlayerAway()
    {
        interactionPrompt.SetActive(false);
    }
}
