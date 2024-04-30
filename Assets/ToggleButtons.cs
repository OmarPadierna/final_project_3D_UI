using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class ToggleButtons : MonoBehaviour
{
    public InputActionReference yButtonActionReference;
    public InputActionReference bButtonActionReference;
    public InputActionReference aButtonActionReference;
    public InputActionReference xButtonActionReference;

    public GameObject minimapObject;
    public GameObject helpPanelObject;
    public GameObject videoPanelObject;
    public LazyFollow lazyFollowComponent;

    private void OnEnable()
    {
        yButtonActionReference.action.performed += OnYButtonPressed;
        bButtonActionReference.action.performed += OnBButtonPressed;
        aButtonActionReference.action.performed += OnAButtonPressed;
        xButtonActionReference.action.performed += OnXButtonPressed;


        yButtonActionReference.action.Enable();
        bButtonActionReference.action.Enable();
        aButtonActionReference.action.Enable();
        xButtonActionReference.action.Enable();
    }

    private void OnDisable()
    {
        // Unsubscribe from the button actions
        yButtonActionReference.action.performed -= OnYButtonPressed;
        bButtonActionReference.action.performed -= OnBButtonPressed;
        aButtonActionReference.action.performed -= OnAButtonPressed;
        xButtonActionReference.action.performed -= OnXButtonPressed;

        yButtonActionReference.action.Disable();
        bButtonActionReference.action.Disable();
        aButtonActionReference.action.Disable();
        xButtonActionReference.action.Disable();
    }

    private void OnYButtonPressed(InputAction.CallbackContext context)
    {
        // Toggle the active state of the minimap object
        minimapObject.SetActive(!minimapObject.activeSelf);
        Debug.Log($"Minimap active state toggled. New state: {minimapObject.activeSelf}");
    }

    private void OnBButtonPressed(InputAction.CallbackContext context)
    {
        // Toggle the position follow mode of the LazyFollow component
        if (lazyFollowComponent != null)
        {
            lazyFollowComponent.positionFollowMode = 
                (lazyFollowComponent.positionFollowMode == LazyFollow.PositionFollowMode.Follow)
                ? LazyFollow.PositionFollowMode.None
                : LazyFollow.PositionFollowMode.Follow;

            Debug.Log($"LazyFollow position follow mode toggled. New mode: {lazyFollowComponent.positionFollowMode}");

            lazyFollowComponent.rotationFollowMode = 
                (lazyFollowComponent.rotationFollowMode == LazyFollow.RotationFollowMode.LookAtWithWorldUp)
                ? LazyFollow.RotationFollowMode.None
                : LazyFollow.RotationFollowMode.LookAtWithWorldUp;

            Debug.Log($"LazyFollow rotation follow mode toggled. New mode: {lazyFollowComponent.rotationFollowMode}");
        }
        else
        {
            Debug.LogError("LazyFollow component not found on the GameObject.");
        }
    }

    private void OnAButtonPressed(InputAction.CallbackContext context)
    {
        // Toggle the active state of the help panel object
        videoPanelObject.SetActive(!videoPanelObject.activeSelf);
        Debug.Log($"Video panel active state toggled. New state: {videoPanelObject.activeSelf}");
    }

    private void OnXButtonPressed(InputAction.CallbackContext context)
    {
        // Toggle the active state of the help panel object
        helpPanelObject.SetActive(!helpPanelObject.activeSelf);
        Debug.Log($"Help panel active state toggled. New state: {helpPanelObject.activeSelf}");
    }
}