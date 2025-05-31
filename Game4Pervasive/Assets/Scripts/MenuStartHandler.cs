using UnityEngine;
using UnityEngine.InputSystem;

public class MenuStartHandler : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference start; // Assign in Inspector

    [Header("Scene")]
    public string mainSceneName = "Main"; // Set your target scene name

    private void OnEnable()
    {
        start.action.Enable();
        start.action.performed += OnStartPressed;
    }

    private void OnDisable()
    {
        start.action.performed -= OnStartPressed;
        start.action.Disable();
    }

    private void OnStartPressed(InputAction.CallbackContext context)
    {
        if (SceneFadeManager.Instance != null)
        {
            SceneFadeManager.Instance.LoadScene(mainSceneName);
        }
        else
        {
            Debug.LogWarning("SceneFadeManager not found! Loading scene directly.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(mainSceneName);
        }
    }
}
