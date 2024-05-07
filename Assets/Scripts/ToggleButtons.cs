using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ToggleButtons : MonoBehaviour
{
    public InputActionReference yButtonActionReference;
    public InputActionReference bButtonActionReference;
    public InputActionReference aButtonActionReference;
    public InputActionReference xButtonActionReference;

    public GameObject minimapObject;
    public GameObject helpPanelObject;
    public GameObject environment;
    public List<VideoPlayer> videoPlayers;

    public TextMeshProUGUI minimapAffordanceText;
    public TextMeshProUGUI helpPanelAffordanceText;
    public TextMeshProUGUI lazyFollowAffordanceText;

    void Start()
    {
        videoPlayers = environment.GetComponent<MovieManager>().videoPlayers;
    }
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
        for (int i = 0; i < videoPlayers.Count; i++)
        {
            LazyFollow lazyFollowComponent = videoPlayers[i].transform.parent.transform.parent.transform.parent.GetComponent<LazyFollow>();
            Rigidbody rb = videoPlayers[i].transform.parent.transform.parent.transform.parent.GetComponent<Rigidbody>();
            XRGrabInteractable xrGrabInteractable = videoPlayers[i].transform.parent.transform.parent.transform.parent.GetComponent<XRGrabInteractable>();

            // Toggle the lazy follow mode
            lazyFollowComponent.rotationFollowMode = 
                (lazyFollowComponent.rotationFollowMode == LazyFollow.RotationFollowMode.LookAtWithWorldUp)
                ? LazyFollow.RotationFollowMode.None
                : LazyFollow.RotationFollowMode.LookAtWithWorldUp;

            // Update the text to reflect the current state
            lazyFollowAffordanceText.text = 
                (lazyFollowComponent.rotationFollowMode == LazyFollow.RotationFollowMode.None)
                ? "Video Panel Follow Mode (Disabled)"
                : "Video Panel Follow Mode (Enabled)";

            // Toggle the Rigidbody constraints and XR Grab Interactable settings
            if (lazyFollowComponent.rotationFollowMode == LazyFollow.RotationFollowMode.None)
            {
                // Disable the freeze rotation constraint for the Y axis
                rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;

                // Set the movement type on the XR grab interactable component to be kinematic
                xrGrabInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;

                // Enable the track rotation setting on the XR grab interactable component
                xrGrabInteractable.trackRotation = true;
            }
            else
            {
                // Enable the freeze rotation constraint for the Y axis
                rb.constraints |= RigidbodyConstraints.FreezeRotationY;

                // Set the movement type on the XR grab interactable component to be instant
                xrGrabInteractable.movementType = XRBaseInteractable.MovementType.Instantaneous;

                // Disable the track rotation setting on the XR grab interactable component
                xrGrabInteractable.trackRotation = false;
            }

            Debug.Log($"LazyFollow modes toggled. New rotation mode: {lazyFollowComponent.rotationFollowMode}");
        }
    }

    private void OnAButtonPressed(InputAction.CallbackContext context)
    {
        for (int i = 0; i < videoPlayers.Count; i++)
        {
            videoPlayers[i].transform.parent.transform.parent.transform.parent.gameObject.SetActive(false);
        }
        environment.GetComponent<MovieManager>().ResetAll();
        Debug.Log($"Video panels cleared.");
    }

    private void OnXButtonPressed(InputAction.CallbackContext context)
    {
        helpPanelObject.SetActive(!helpPanelObject.activeSelf);
        helpPanelAffordanceText.text = helpPanelObject.activeSelf ? "Help Panel (Visible)" : "Help Panel (Hidden)";
        Debug.Log($"Help panel active state toggled. New state: {helpPanelObject.activeSelf}");
    }
}