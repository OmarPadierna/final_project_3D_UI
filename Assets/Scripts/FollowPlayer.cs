using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform vrCamera; 
    public float distanceInFrontOfCamera = 2.0f; 

    void Update()
    {

        if (vrCamera == null)
        {
            Debug.LogError("VR Camera is not assigned!");
            return;
        }

        Vector3 desiredPosition = vrCamera.position + vrCamera.forward * distanceInFrontOfCamera;
        transform.position = desiredPosition;
        transform.rotation = Quaternion.LookRotation(transform.position - vrCamera.position);
    }
}
