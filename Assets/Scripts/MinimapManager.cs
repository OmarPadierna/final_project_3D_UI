using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField]
    public Transform minimapOrigin;
    [SerializeField]
    public Transform environmentOrigin;
    [SerializeField]
    public Transform playerReferenceTransform;
    [SerializeField]
    public Transform playerTransform;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //playerTransform = FindObjectOfType<UnityEngine.XR.Interaction.Toolkit.XRBaseController>().transform;
    }

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
    }
}
