using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DarkenParticleSystem : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private LightingManager lightingManager;
    private float originalEmissionRate = -1f; // Uninitialized

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (lightingManager == null)
        {
            lightingManager = FindFirstObjectByType<LightingManager>();
            if (lightingManager == null)
                return;
        }

        var emission = particleSystem.emission;

        // Cache the original emission rate once
        if (originalEmissionRate < 0f)
        {
            originalEmissionRate = emission.rateOverTime.constant;
        }

        float time = lightingManager.TimeOfDay;
        float emissionMultiplier = CalculateEmissionMultiplier(time);
        emission.rateOverTime = originalEmissionRate * emissionMultiplier;
    }

    private float CalculateEmissionMultiplier(float time)
    {
        // Morning fade in: 7 AM to 10 AM
        if (time >= 7f && time < 10f)
        {
            return Mathf.Lerp(0f, 1f, (time - 7f) / 3f);
        }

        // Evening fade out: 4 PM to 7 PM
        if (time >= 16f && time < 17f)
        {
            return Mathf.Lerp(1f, 0f, (time - 16f) / 3f);
        }

        // Full emission between 10 AM and 4 PM
        if (time >= 10f && time < 16f)
        {
            return 1f;
        }

        // No emission otherwise (night)
        return 0f;
    }
}
