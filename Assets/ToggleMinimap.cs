using UnityEngine;
using UnityEngine.InputSystem;
public class ToggleMinimap : MonoBehaviour
{
    public InputActionReference yButtonActionReference;
    public GameObject objectToToggle; // Reference to the serialized GameObject

    private void OnEnable()
    {
        yButtonActionReference.action.performed += OnYButtonPressed;
        yButtonActionReference.action.Enable();
    }

    private void OnDisable()
    {
        yButtonActionReference.action.performed -= OnYButtonPressed;
        yButtonActionReference.action.Disable();
    }

    private void OnYButtonPressed(InputAction.CallbackContext context)
    {
        objectToToggle.SetActive(!objectToToggle.activeSelf);
        Debug.Log($"Object active state toggled. New state: {objectToToggle.activeSelf}");
    }
}