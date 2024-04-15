using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipInteractable : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("[Filter me]: Tooltip interacted with!");
    }
}
