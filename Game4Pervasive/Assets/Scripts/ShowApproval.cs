using UnityEngine;

public class ShowApproval : MonoBehaviour
{
    // npc ref
    public NPC npc;

    // approval icon prefab
    public GameObject approvalIconPrefab;

    // container
    public Transform iconContainer;

    void Start()
    {

    }

    void ClearContainer()
    {
        // Clear the icon container
        foreach (Transform child in iconContainer)
        {
            Destroy(child.gameObject);
        }
    }

    void CreateApprovalIcons(int approvalPoints)
    {
        // create new approval icons based on approval points
        for (int i = 0; i < approvalPoints; i++)
        {
            Instantiate(approvalIconPrefab, iconContainer);
        }
    }

    public void ShowApprovalIcons()
    {
        ClearContainer();

        int approvalPoints = npc.GetCharacterApprovalPoints();

        Debug.Log("Approval Points: " + approvalPoints);

        CreateApprovalIcons(approvalPoints);
    }
}
