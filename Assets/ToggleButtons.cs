using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.UI;
using TMPro;

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

    public TextMeshProUGUI minimapAffordanceText;
    public TextMeshProUGUI helpPanelAffordanceText;
    public TextMeshProUGUI lazyFollowAffordanceText;
    public TextMeshProUGUI videoPanelAffordanceText;

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
        minimapObject.SetActive(!minimapObject.activeSelf);
        minimapAffordanceText.text = minimapObject.activeSelf ? "Minimap (Visible)" : "Minimap (Hidden)";
        Debug.Log($"Minimap active state toggled. New state: {minimapObject.activeSelf}");
    }

    private void OnBButtonPressed(InputAction.CallbackContext context)
    {
        if (lazyFollowComponent != null)
        {
            lazyFollowComponent.positionFollowMode = 
                (lazyFollowComponent.positionFollowMode == LazyFollow.PositionFollowMode.Follow)
                ? LazyFollow.PositionFollowMode.None
                : LazyFollow.PositionFollowMode.Follow;

            lazyFollowComponent.rotationFollowMode = 
                (lazyFollowComponent.rotationFollowMode == LazyFollow.RotationFollowMode.LookAtWithWorldUp)
                ? LazyFollow.RotationFollowMode.None
                : LazyFollow.RotationFollowMode.LookAtWithWorldUp;

            lazyFollowAffordanceText.text = 
                (lazyFollowComponent.positionFollowMode == LazyFollow.PositionFollowMode.Follow)
                ? "Video Panel Follow Mode (Enabled)"
                : "Video Panel Follow Mode (Disabled)";

            Debug.Log($"LazyFollow modes toggled. New position mode: {lazyFollowComponent.positionFollowMode}, New rotation mode: {lazyFollowComponent.rotationFollowMode}");
        }
        else
        {
            Debug.LogError("LazyFollow component not found on the GameObject.");
        }
    }

    private void OnAButtonPressed(InputAction.CallbackContext context)
    {
        videoPanelObject.SetActive(!videoPanelObject.activeSelf);
        videoPanelAffordanceText.text = videoPanelObject.activeSelf ? "Video Panel (Visible)" : "Video Panel (Hidden)";
        Debug.Log($"Video panel active state toggled. New state: {videoPanelObject.activeSelf}");
    }

    private void OnXButtonPressed(InputAction.CallbackContext context)
    {
        helpPanelObject.SetActive(!helpPanelObject.activeSelf);
        helpPanelAffordanceText.text = helpPanelObject.activeSelf ? "Help Panel (Visible)" : "Help Panel (Hidden)";
        Debug.Log($"Help panel active state toggled. New state: {helpPanelObject.activeSelf}");
    }
}