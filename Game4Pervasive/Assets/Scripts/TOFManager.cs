using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEditor.SearchService;

public class TOFManager : MonoBehaviour
{
    [System.Serializable]
    public struct TOFCharacter
    {
        public string characterName;
        public GameObject character;
    }

    private TOFData tofData;
    public TOF tof;

    public TOFCharacter[] tofCharacters;

    [Header("UI References")]
    public TMP_Text dialogueText;
    public TMP_Text characterText;
    public Transform optionsContainer;
    public GameObject optionButtonPrefab;
    public GameObject heartContainer;
    public GameObject heartPrefab;
    public GameObject correctPrefab;
    public GameObject incorrectPrefab;
    public GameObject spawner;

    private int currentDoubtIndex = 0;
    private int approvalRating = 3;
    private string currentDialogueText = string.Empty;
    private bool awaitingPlayerInputAfterResponse = false;

    public InputActionReference next;
    private Coroutine typingCoroutine = null;
    private Action onTextComplete = null;

    void Start()
    {
        tofData = Array.Find(tof.tofs, (t) => t.name == SceneFadeManager.CurrentCharacterTOF).data;
        characterText.text = tofData.characterName;
        TOFCharacter character = Array.Find(tofCharacters, (c) => c.characterName == tofData.characterName);
        character.character.SetActive(true);
        Invoke("DisplayCurrentDoubt", 2f);
        UpdateApprovalRating();
    }

    void Update()
    {
        if (next.action.triggered)
        {
            if (dialogueText.text != currentDialogueText)
            {
                // Skip typing animation
                dialogueText.text = currentDialogueText;
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                    onTextComplete?.Invoke();
                    typingCoroutine = null;
                }
            }
            else if (awaitingPlayerInputAfterResponse)
            {
                awaitingPlayerInputAfterResponse = false;
                DisplayCurrentDoubt();
            }
        }
    }

    void DisplayCurrentDoubt()
    {
        if (currentDoubtIndex >= tofData.doubts.Length)
        {
            SceneFadeManager.Instance.LoadScene("Main");
            return;
        }

        var doubt = tofData.doubts[currentDoubtIndex];
        TypeDialogueText(doubt.doubtText, DisplayOptions);
    }

    void DisplayOptions()
    {
        ClearOptions();
        var doubt = tofData.doubts[currentDoubtIndex];

        foreach (var option in doubt.options)
        {
            bool isUnlocked = true;
            foreach (var item in option.itemsRequired)
            {
                if (!Inventory.Instance.HasItem(item))
                {
                    isUnlocked = false;
                    break;
                }
            }

            if (!isUnlocked) continue;

            var buttonGO = Instantiate(optionButtonPrefab, optionsContainer);
            var buttonText = buttonGO.GetComponentInChildren<TMP_Text>();
            var button = buttonGO.GetComponent<Button>();

            buttonText.text = option.option;

            int optionId = option.optionId;
            button.onClick.AddListener(() => OnOptionSelected(optionId));
        }

        if (optionsContainer.childCount > 0)
        {
            var firstButton = optionsContainer.GetChild(0).GetComponent<Button>();
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        }
    }

    void TypeDialogueText(string text, Action onComplete)
    {
        onTextComplete = onComplete;
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypingCoroutine(text));
    }

    IEnumerator TypingCoroutine(string text)
    {
        currentDialogueText = text;
        dialogueText.text = string.Empty;

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        typingCoroutine = null;
        onTextComplete?.Invoke();
    }

    void OnOptionSelected(int selectedOptionId)
    {
        var doubt = tofData.doubts[currentDoubtIndex];
        ClearOptions();

        string response;
        if (selectedOptionId == doubt.correctOptionId)
        {
            // Correct option selected
            var correctGO = Instantiate(correctPrefab);
            correctGO.transform.SetParent(spawner.transform);
            // Set position to spawner's position
            correctGO.transform.localPosition = Vector3.zero;
            response = doubt.doubtSuccessResponse;
            approvalRating = Mathf.Min(10, approvalRating + 1);
        }
        else
        {
            // Incorrect option selected
            var incorrectGO = Instantiate(incorrectPrefab);
            incorrectGO.transform.SetParent(spawner.transform);
            // Set position to spawner's position
            incorrectGO.transform.localPosition = Vector3.zero;
            response = doubt.doubtFailureResponse;
            approvalRating = Mathf.Max(1, approvalRating - 1);
        }

        UpdateApprovalRating();
        currentDoubtIndex++;

        // Show response text, then wait for player to press next
        TypeDialogueText(response, () => awaitingPlayerInputAfterResponse = true);
    }

    void UpdateApprovalRating()
    {
        // Clear existing hearts
        foreach (Transform child in heartContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // Create new hearts based on approval rating
        for (int i = 0; i < approvalRating; i++)
        {
            var heartGO = Instantiate(heartPrefab, heartContainer.transform);
            heartGO.SetActive(true);
        }
    }

    void ClearOptions()
    {
        foreach (Transform child in optionsContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
