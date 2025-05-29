using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    public Light DirectionalLight;
    public float RotationSpeed = 1f; // Speed of rotation in degrees per second
    public LightingPreset Preset;
    [SerializeField, Range(0, 24)]
    [HideInInspector] public float TimeOfDay; // 0 to 24, where 0 is midnight and 24 is the next midnight

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime / RotationSpeed;
            TimeOfDay %= 24; // Wrap around after 24 hours
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0f));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
        {
            return;
        }

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsByType<Light>(FindObjectsSortMode.None);
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    break;
                }
            }
        }
    }
}
