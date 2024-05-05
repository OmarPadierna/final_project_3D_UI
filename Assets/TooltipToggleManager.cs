using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TooltipToggleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tooltips;
    [SerializeField]
    private GameObject videoReferences;
    [SerializeField]
    private Toggle roomsToggle;
    [SerializeField]
    private Toggle videosToggle;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        roomsToggle.isOn = false;
        videosToggle.isOn = false;
    }

    public void OnRoomsToggleChanged() {
        if (roomsToggle.isOn) {
            videosToggle.isOn = false;
            tooltips.SetActive(true);
        } else {
            tooltips.SetActive(false);
        }
    }

    public void OnVideosToggleChanged() {
        if (videosToggle.isOn) {
            roomsToggle.isOn = false;
            videoReferences.SetActive(true);
        } else {
            videoReferences.SetActive(false);
        }
    }
}
