using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneFadeManager : MonoBehaviour
{
    public static SceneFadeManager Instance;
    public static string CurrentCharacterTOF = "Tia"; // Default TOF character, can be changed

    [Header("Fade Settings")]
    public Image fadeImage; // Assign in inspector
    public Animator fadeAnimator; // Animator on FadeImage
    public float fadeDuration = 1f;

    private bool isFading = false;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(LoadSceneRoutine(sceneName));
        }
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        isFading = true;

        // Start fade out
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(fadeDuration);

        // Load scene
        SceneManager.LoadScene(sceneName);

        // Wait one frame to allow scene load
        yield return null;

        // Start fade in
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(fadeDuration);

        isFading = false;
    }
}
