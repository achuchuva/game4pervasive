using System.IO;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{

    public CharacterDataObj LoadCharacter(string fileName)
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<CharacterDataObj>(json);
        }
        Debug.LogError("File not found: " + path);
        return null;
    }
}