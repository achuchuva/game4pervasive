using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Lighting Preset", menuName = "RPG/LightingPreset")]
public class LightingPreset : ScriptableObject
{
    public Gradient AmbientColor; // Gradient for ambient color over time
    public Gradient DirectionalColor; // Gradient for directional light color over time
    public Gradient FogColor; // Gradient for fog color over time
}
