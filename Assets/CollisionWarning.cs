using UnityEngine;

public class CollisionUIController : MonoBehaviour
{
    [SerializeField] private Canvas collisionCanvas;

    private void OnCollisionEnter(Collision collision)
    {
        // Activate the canvas when the object starts colliding
        collisionCanvas.gameObject.SetActive(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        // Deactivate the canvas when the collision stops
        collisionCanvas.gameObject.SetActive(false);
    }
}