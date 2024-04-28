using UnityEngine;
using UnityEngine.InputSystem;

public class HelpPanelController : MonoBehaviour
{
    public GameObject objectToToggle;
    public InputActionReference aButtonActionReference;

    private void Start()
    {
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(false);
        }
    }

    private void OnEnable()
    {
        aButtonActionReference.action.performed += OnAButtonPressed;
        aButtonActionReference.action.Enable();
    }

    private void OnDisable()
    {
        aButtonActionReference.action.performed -= OnAButtonPressed;
        aButtonActionReference.action.Disable();
    }

    private void OnAButtonPressed(InputAction.CallbackContext context)
    {
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(!objectToToggle.activeSelf);
            Debug.Log($"Object active state toggled. New state: {objectToToggle.activeSelf}");
        }
    }
}
