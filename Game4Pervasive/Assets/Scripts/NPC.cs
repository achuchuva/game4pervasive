using UnityEngine;

public class NPC : MonoBehaviour
{
    public CharacterData characterData;
    public GameObject interactionPrompt;

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
