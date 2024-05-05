using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VideoTooltipTeleportManager : MonoBehaviour
{
    [SerializeField]
    private TeleportationProvider teleportationProvider;
    [SerializeField]
    public Transform cameraOffset;
    [SerializeField]
    public Transform minimapOrigin;
    [SerializeField]
    public Transform environmentOrigin;
    [SerializeField]
    public Transform videoTrackerTransform;

    [SerializeField]
    private List<GameObject> rooms;
    private List<Bounds> bounds;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("[Filter me]: Calculating bounds!");

        if (rooms.Count <= 0) {
            Debug.Log("[Filter me]: rooms list is empty!");
        } else {
            Debug.Log("[Filter me]: rooms list is not empty!");
        }

        bounds = new List<Bounds>(); // Initialize the list

        for (int i = 0; i < rooms.Count; i++)
        {
            Debug.Log("[Filter me]: iterating over rooms!");
            GameObject room = rooms[i];
             Bounds bound = CalculateBound(room);
             bounds.Add(bound); // Add the bound to the list
        }
    }

    public void OnTooltipClicked() {

        Debug.Log("[Filter me]: Tooltip clicked!");

        Vector3 videoPosition = GetEnvironmentPosition(videoTrackerTransform.position);

        if (bounds.Count <= 0) {
            Debug.Log("[Filter me]: bounds is empty");
        } else {
            Debug.Log("[Filter me]: bounds is not empty");
        }

        for (int i = 0; i < bounds.Count; i++)
        {
            Debug.Log("[Filter me]: traversing bounds");
            Bounds bound = bounds[i];

            if (bound.Contains(videoPosition)){
                Debug.Log("[Filter me]: video position found bound");
                GameObject room = rooms[i];
                Transform targetRoomCenter = room.transform.Find("RoomCenter");
                if (targetRoomCenter != null) {
                    TeleportToVideo(targetRoomCenter);
                    break;
                } else {
                    Debug.Log("[Filter me]: no target room center found!");
                }
                
            } else {
                Debug.Log("[Filter me]: No bounds found");
            }
        } 
    }

    public Vector3 GetEnvironmentPosition(Vector3 position) {
        Vector3 environmentLocalPosition = minimapOrigin.InverseTransformPoint(position);
        return environmentOrigin.TransformPoint(environmentLocalPosition);
    }

    public void TeleportToVideo(Transform targetRoom)
    {
         if (teleportationProvider == null)
        {
            Debug.LogError("[Filter me]: TeleportationProvider is not set.");
            return;
        }

        Debug.Log("[Filter me]: Target room Position " + targetRoom.position);

        Vector3 targetPosition = targetRoom.position;
        //targetPosition.y -= 0.6f; //Magic number obtained through debugging. Not sure why there's a constant offset of 0.6f;

        TeleportRequest request = new TeleportRequest()
        {
            
            destinationPosition = targetPosition,
        };

        teleportationProvider.QueueTeleportRequest(request);

    }

    private Bounds CalculateBound(GameObject room)
    {
        var renderers = room.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return new Bounds(room.transform.position, Vector3.zero);

        Bounds bounds = renderers[0].bounds;
        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
        return bounds;

    }


}
