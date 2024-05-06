using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tooltips;
    [SerializeField]
    private GameObject videoReferences;
    [SerializeField]
    private Button roomsButton;
    [SerializeField]
    private Button videosButton;
    private ColorBlock roomsColor;
    private ColorBlock videosColor;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        tooltips.SetActive(true);
        videoReferences.SetActive(false);
        roomsColor = roomsButton.colors;
        videosColor = videosButton.colors;
        roomsColor.normalColor = new Color(0,0,0, 0.9f);
        roomsButton.colors = roomsColor;
    }

    public void OnRoomsButtonClick()
    {
        if (!tooltips.activeSelf)
        {
            tooltips.SetActive(true);
            videoReferences.SetActive(false);
            roomsColor.normalColor = new Color(0,0,0, 0.9f);
            roomsButton.colors = roomsColor;
            videosColor.normalColor = new Color(0,0,0, 0f);
            videosButton.colors = videosColor;
        }
    }

    public void OnVideosButtonClick()
    {
        if (!videoReferences.activeSelf)
        {
            tooltips.SetActive(false);
            videoReferences.SetActive(true);
            roomsColor.normalColor = new Color(0,0,0, 0f);
            roomsButton.colors = roomsColor;
            videosColor.normalColor = new Color(0,0,0, 0.9f);
            videosButton.colors = videosColor;
        }
    }
}