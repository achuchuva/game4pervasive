using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "RPG/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite portrait;
    public string backstory;
    public int approvalPoints;
    public int maxApprovalPoints = 10;

    public GameObject[] unlockableDecorations;
    public string[] unlockedInteractions;
}
