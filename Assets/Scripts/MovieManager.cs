using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.Video;

public class MovieManager : MonoBehaviour
{
    public List<VideoPlayer> videoPlayers;
    public List<VideoClip> videoClips;
    public GameObject videoPlayerCanvas;
    public GameObject messageCanvas; // Reference to the canvas that contains the message
    public bool[] coordinateAvailable = new bool[3] { true, true, true };
    public int[] playerToCoordinateIndex = new int[3] { -1, -1, -1 };
    public List<Transform> teleportCoordinates;
    private int activePlayerCount = 0;
    private Vector3[] originalPositions;


    void Start()
    {
        // Initialize the array to the same size as the videoPlayers list
        originalPositions = new Vector3[videoPlayers.Count];

        // Store the original positions of the video players
        for (int i = 0; i < videoPlayers.Count; i++)
        {
            originalPositions[i] = videoPlayers[i].transform.parent.transform.parent.transform.parent.position;
        }
    }
    public void PlayVideo(int index)
    {
        if (activePlayerCount == 3)
        {
            StartCoroutine(DisplayMessage());
            return;
        }

        

        if (index >= 0 && index < videoClips.Count && index < videoPlayers.Count)
        {
            if (!videoPlayers[index].transform.parent.transform.parent.transform.parent.gameObject.activeSelf)
            {
                videoPlayers[index].transform.parent.transform.parent.transform.parent.gameObject.SetActive(true);
                videoPlayers[index].clip = videoClips[index];
                activePlayerCount++;
                videoPlayers[index].GetComponent<VideoTimeScrubControl>().VideoStop();
                Debug.Log("Loading video: " + videoClips[index].name);

                int coordinateIndex = -1;
                for (int i = 0; i < teleportCoordinates.Count; i++)
                {
                    if (coordinateAvailable[i])
                    {
                        coordinateIndex = i;
                        coordinateAvailable[i] = false; // Mark the coordinate as used
                        break;
                    }
                }
                playerToCoordinateIndex[coordinateIndex] = index; // Associate the player with its index
            }
        }
        else
        {
            Debug.LogError("Index out of range");
        }
    }

    IEnumerator DisplayMessage()
    {
        messageCanvas.SetActive(true);
        yield return new WaitForSeconds(2);
        messageCanvas.SetActive(false);
    }


    public void TeleportAllVideoPlayers()
    {
        int tempTracker = 0; // Temporary tracker variable

        for (int i = 0; i < teleportCoordinates.Count; i++)
        {
            // Teleport the active video player to the corresponding coordinate
            videoPlayers[playerToCoordinateIndex[i]].transform.parent.transform.parent.transform.parent.position = teleportCoordinates[i].position;
            tempTracker++; // Increment the tracker
        }
    }

    public void TeleportVideoPlayer(int playerIndex)
    {
        int coordinateIndex = -1;

        // Check for the first available coordinate
        for (int i = 0; i < teleportCoordinates.Count; i++)
        {
            if (playerToCoordinateIndex[i] == playerIndex)
            {
                coordinateIndex = i;
                break;
            }
        }

        if (coordinateIndex != -1)
        {
            videoPlayers[playerIndex].transform.parent.transform.parent.transform.parent.position = teleportCoordinates[coordinateIndex].position;
        }
        else
        {
            Debug.LogError("No available coordinates to teleport the video player.");
        }
    }

    // Call this method when a video player is set to inactive
    public void ResetCoordinate(int playerIndex)
    {
        int coordinateIndex = -1;
        for (int i = 0; i < teleportCoordinates.Count; i++)
        {
            if (playerToCoordinateIndex[i] == playerIndex)
            {
                coordinateIndex = i;
                break;
            }
        }
        if (coordinateIndex != -1)
        {
            coordinateAvailable[coordinateIndex] = true; // Reset the availability of the specific coordinate
            playerToCoordinateIndex[coordinateIndex] = -1; // Clear the association
        }
    }

    public void CloseVideoPlayer(int index)
    {
        activePlayerCount--;
        videoPlayers[index].transform.parent.transform.parent.transform.parent.position = originalPositions[index];
        videoPlayers[index].Stop();
        videoPlayers[index].transform.parent.transform.parent.transform.parent.gameObject.SetActive(false);

        // Reset the coordinate availability and decrement the active player count
        ResetCoordinate(index);
    }

    public void ResetAll()
    {
        // Reset the active player count
        activePlayerCount = 0;

        // Reset the coordinate availability
        for (int i = 0; i < coordinateAvailable.Length; i++)
        {
            coordinateAvailable[i] = true;
        }

        // Clear the player to coordinate index associations
        for (int i = 0; i < playerToCoordinateIndex.Length; i++)
        {
            playerToCoordinateIndex[i] = -1;
        }

        for (int i = 0; i < videoPlayers.Count; i++)
        {
            videoPlayers[i].transform.parent.transform.parent.transform.parent.position = originalPositions[i];
        }
    }
}
