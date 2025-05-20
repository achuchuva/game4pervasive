using UnityEngine;

public class NPC : MonoBehaviour
{
    public CharacterData characterData;
    public GameObject interactionPrompt;

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
