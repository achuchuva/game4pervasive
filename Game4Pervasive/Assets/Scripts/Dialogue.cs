using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed;
    public Image titleImage;
    public TextMeshProUGUI titleText;

    public GameObject options;
    public Selectable option;

    public GameObject optionPrefab;

    private int index;
    private string[] lines;

    public InputActionReference nextLine;

    // current node
    public DialogueNode currentNode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (nextLine.action.triggered)
        {
            // if we are already showing options, ignore this, we are actually choosing the option
            if (options.activeSelf)
            {
                return;
            }

            // regardless if the dialogue is finished or not, we want to show options if its the last line
            if (HasOptions() && LastLine())
            {
                ShowOptions();
            }

            if (textComponent.text == lines[index])
            {
                if (!LastLine())
                {
                    NextLine();
                }
                else
                {
                    // otherwise, end the conversation
                    EndConversation();
                }
            }
            else
            {
                // otherwise, skip to the end of the current line
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public bool HasOptions()
    {
        return currentNode.branches.Length > 0;
    }

    public void ShowOptions()
    {
        options.SetActive(true);
    }

    public void ClearOptions()
    {
        foreach (Transform child in options.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void CreateOptionsFromNode(DialogueNode node)
    {
        // create a button for each option
        foreach (DialogueNode branch in node.branches)
        {
            GameObject newOption = Instantiate(optionPrefab, options.transform);
            Button button = newOption.GetComponent<Button>();
            TextMeshProUGUI buttonText = newOption.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = branch.optionName;
            button.onClick.AddListener(() =>
            {
                Debug.Log("Clicked option: " + branch.optionName);
                StartDialogue(branch);
            });
        }
    }

    public void EndConversation()
    {
        index = 0;
        DialogueManager.Instance.isDialogueActive = false;
        lines = null;
        gameObject.SetActive(false);
        options.SetActive(false);
    }

    public void StartDialogue(DialogueNode dialogueRootNode)
    {
        DialogueManager.Instance.isDialogueActive = true;

        // clear options
        ClearOptions();

        index = 0;
        textComponent.text = string.Empty;
        lines = dialogueRootNode.lines;

        // hide options
        options.SetActive(false);

        // set the current node
        currentNode = dialogueRootNode;

        // create the lines
        if (dialogueRootNode.branches.Length > 0)
        {
            CreateOptionsFromNode(dialogueRootNode);

            // select the first option
            option = options.GetComponentInChildren<Selectable>();
            option.Select();
        }

        StartCoroutine(TypeLine());
    }

    private bool LastLine()
    {
        return index == lines.Length - 1;
    }

    IEnumerator TypeLine()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        // show options if there are any
        if (HasOptions() && LastLine())
        {
            ShowOptions();
        }

    }

    private bool DialogueFinished()
    {
        return index >= lines.Length - 1;
    }


    void NextLine()
    {
        if (!DialogueFinished())
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }
}
