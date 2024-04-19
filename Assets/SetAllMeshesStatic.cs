using UnityEngine;

public class SetAllMeshesStatic : MonoBehaviour
{
    void Start()
    {
        // Get all GameObjects in the scene
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        // Loop through each GameObject
        foreach (GameObject obj in allObjects)
        {
            // Check if the GameObject has a MeshRenderer component
            MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                // Set the GameObject as static
                obj.isStatic = true;
            }
        }
    }
}
