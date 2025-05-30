using UnityEngine;

public class CharMenuManager : MonoBehaviour
{

    // list of approval objects
    public ShowApproval[] approvalObjects;

    void Start()
    {

    }

    // show menu
    public void ShowCharacterMenu()
    {
        // loop through all approval objects and show their icons
        foreach (ShowApproval approvalObject in approvalObjects)
        {
            approvalObject.ShowApprovalIcons();
        }
    }
}
