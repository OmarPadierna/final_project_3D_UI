using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class MinimapManager : MonoBehaviour
{
    [SerializeField]
    public Transform minimapOrigin;
    [SerializeField]
    public Transform environmentOrigin;
    [SerializeField]
    public Transform playerReferenceTransform;

    public List<Transform> videoMarkers;
    public List<TextMeshProUGUI> markerText;
    [SerializeField]
    public Transform playerTransform;
    public GameObject environment;

    // Update is called once per frame
    void Update()
    {
        //Convert from world coordinates to environment coordinates. 
        Vector3 playerEnvironmentTransform = environmentOrigin.InverseTransformPoint(playerTransform.position); 
        
        //Change the markers position to the player's environment position and use the minimap Origin to convert from environment coords to minimap coords.
        playerReferenceTransform.position = minimapOrigin.TransformPoint(playerEnvironmentTransform);

        //Update roation (i.e. where the user is looking at)
        Quaternion relativeRotation = Quaternion.Inverse(environmentOrigin.rotation) * playerTransform.rotation;
        playerReferenceTransform.rotation = minimapOrigin.rotation * relativeRotation;

        int[] playerToCoordinateIndex = environment.GetComponent<MovieManager>().playerToCoordinateIndex;
        for (int i = 0; i < playerToCoordinateIndex.Length; i++)
        {
            int playerind = playerToCoordinateIndex[i];
            if (playerind == -1) {
                videoMarkers[i].gameObject.SetActive(false);
            } else {
                videoMarkers[i].gameObject.SetActive(true);
                VideoPlayer player = environment.GetComponent<MovieManager>().videoPlayers[playerToCoordinateIndex[i]];
                double progressPercent = player.time / player.length * 100;
                markerText[i].text = $"{player.name} - {progressPercent:F0}%";
                Vector3 videoenv = environmentOrigin.InverseTransformPoint(player.transform.parent.transform.parent.transform.parent.position); 
                videoMarkers[i].position = minimapOrigin.TransformPoint(videoenv);
            }
        }
    }
}
