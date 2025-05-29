using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class DarkenSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private LightingManager lightingManager;

    [Tooltip("Darkening amount at night and twilight (0 = no change, 1 = fully black)")]
    [Range(0f, 1f)]
    public float MaxDarkenAmount = 0.5f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (lightingManager == null)
        {
            lightingManager = FindObjectOfType<LightingManager>();
            if (lightingManager == null)
                return;
        }

        float time = lightingManager.TimeOfDay;
        float darkenFactor = CalculateDarkenFactor(time);

        // Blend the color toward black based on the darken factor
        Color originalColor = Color.white; // Assuming default is fully lit
        Color darkenedColor = Color.Lerp(originalColor, Color.black, darkenFactor * MaxDarkenAmount);

        Color currentColor = spriteRenderer.color;
        spriteRenderer.color = new Color(darkenedColor.r, darkenedColor.g, darkenedColor.b, currentColor.a);
    }

    private float CalculateDarkenFactor(float time)
    {
        // Dawn: 5 AM to 8 AM (brightens)
        if (time >= 5f && time < 8f)
        {
            return Mathf.Lerp(1f, 0f, (time - 5f) / 3f); // 1 to 0
        }

        // Dusk: 5 PM to 8 PM (darkens)
        if (time >= 17f && time < 20f)
        {
            return Mathf.Lerp(0f, 1f, (time - 17f) / 3f); // 0 to 1
        }

        // Full brightness during day
        if (time >= 8f && time < 17f)
        {
            return 0f;
        }

        // Full dark at night
        return 1f;
    }
}
