using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.UI;
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

            lazyFollowComponent.rotationFollowMode = 
                (lazyFollowComponent.rotationFollowMode == LazyFollow.RotationFollowMode.LookAtWithWorldUp)
                ? LazyFollow.RotationFollowMode.None
                : LazyFollow.RotationFollowMode.LookAtWithWorldUp;

            lazyFollowAffordanceText.text = 
                (lazyFollowComponent.positionFollowMode == LazyFollow.PositionFollowMode.Follow)
                ? "Video Panel Follow Mode (Enabled)"
                : "Video Panel Follow Mode (Disabled)";

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