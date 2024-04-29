using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.Video;

public class MovieManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public List<VideoClip> videoClips;
    public GameObject videoPlayerCanvas;
    public void PlayVideo(int index)
    {
        if (index >= 0 && index < videoClips.Count)
        {
            videoPlayerCanvas.SetActive(true);
            videoPlayer.clip = videoClips[index];
            videoPlayer.GetComponent<VideoTimeScrubControl>().VideoStop();
            Debug.Log("Loading video: " + videoClips[index].name);
        }
        else
        {
            Debug.LogError("Index out of range");
        }
    }
}
