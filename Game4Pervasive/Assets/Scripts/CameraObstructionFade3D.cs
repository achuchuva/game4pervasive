using UnityEngine;
using System.Collections.Generic;

public class CameraObstructionFade3D : MonoBehaviour
{
    public Transform player;
    public float fadedAlpha = 0.3f;
    public float fadeSpeed = 5f;
    public LayerMask obstructionMask;

    private HashSet<SpriteRenderer> currentObstructions = new HashSet<SpriteRenderer>();
    private Dictionary<SpriteRenderer, float> originalAlphas = new Dictionary<SpriteRenderer, float>();

    void Update()
    {
        HandleObstructions();
    }

    void HandleObstructions()
    {
        // Restore sprites that are no longer obstructing
        foreach (var sr in originalAlphas.Keys)
        {
            if (!currentObstructions.Contains(sr) && sr != null)
            {
                Color color = sr.color;
                color.a = Mathf.MoveTowards(color.a, originalAlphas[sr], Time.deltaTime * fadeSpeed);
                sr.color = color;
            }
        }

        currentObstructions.Clear();

        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(
            transform.position,
            direction.normalized,
            distance,
            obstructionMask,
            QueryTriggerInteraction.Collide
        );

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform == player)
                continue;

            // Get all SpriteRenderers on the object, its parents, and children
            HashSet<SpriteRenderer> renderers = new HashSet<SpriteRenderer>();

            // Self
            SpriteRenderer selfSR = hit.transform.GetComponent<SpriteRenderer>();
            if (selfSR != null) renderers.Add(selfSR);

            // Parents
            Transform current = hit.transform.parent;
            while (current != null)
            {
                SpriteRenderer parentSR = current.GetComponent<SpriteRenderer>();
                if (parentSR != null) renderers.Add(parentSR);
                current = current.parent;
            }

            // Children
            SpriteRenderer[] childSRs = hit.transform.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (var sr in childSRs)
            {
                if (sr != null) renderers.Add(sr);
            }

            // Fade all collected renderers
            foreach (var sr in renderers)
            {
                if (!originalAlphas.ContainsKey(sr))
                    originalAlphas[sr] = sr.color.a;

                Color color = sr.color;
                color.a = Mathf.MoveTowards(color.a, fadedAlpha, Time.deltaTime * fadeSpeed);
                sr.color = color;

                currentObstructions.Add(sr);
            }
        }
    }
}
