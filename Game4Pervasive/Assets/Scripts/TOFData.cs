using UnityEngine;

[CreateAssetMenu(fileName = "TOFData", menuName = "RPG/TOFData")]
public class TOFData : ScriptableObject
{
    [System.Serializable]
    public struct Option
    {
        public int optionId;
        public string option;
        public ItemData[] itemsRequired;
    }

    [System.Serializable]
    public struct Doubt
    {
        public int doubtId;
        public string doubtText;
        public Option[] options;
        public int correctOptionId;
        public string doubtSuccessResponse;
        public string doubtFailureResponse;
    }

    public string characterName;
    public Doubt[] doubts;

}
