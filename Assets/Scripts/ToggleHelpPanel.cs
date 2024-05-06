using UnityEngine;
using UnityEngine.InputSystem;
public class ToggleHelpPanel : MonoBehaviour
{
    public InputActionReference aButtonActionReference;
    public GameObject objectToToggle; // Reference to the serialized GameObject

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
        objectToToggle.SetActive(!objectToToggle.activeSelf);
        Debug.Log($"Object active state toggled. New state: {objectToToggle.activeSelf}");
    }
}