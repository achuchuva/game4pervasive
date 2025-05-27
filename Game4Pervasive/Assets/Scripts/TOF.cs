using UnityEngine;

public class TOF : MonoBehaviour
{
    [System.Serializable]
    public struct TOFObject
    {
        public string name;
        public TOFData data;
    }

    public TOFObject[] tofs;
}
