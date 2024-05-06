using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TooltipNavigationManager : MonoBehaviour
{
    [SerializeField]
    private TeleportationProvider teleportationProvider;
    [SerializeField]
    public Transform cameraOffset;
    [SerializeField]
    public Transform horrorRoom;
    [SerializeField]
    public Transform comedyRoom;
    [SerializeField]
    public Transform kidsRoom;
    [SerializeField]
    public Transform userRoom;
    [SerializeField]
    public Transform foreignFilmsRoom;
    [SerializeField]
    public Transform romanticRoom;
    [SerializeField]
    public Transform scifiRoom;
    [SerializeField]
    public Transform classicsRoom;
    [SerializeField]
    public Transform mysteryRoom;

    public void OnHorrorTooltipClicked()
    {
        TeleportPlayer(horrorRoom);
    }

    public void OnComedyTooltipClicked()
    {
        TeleportPlayer(comedyRoom);
    }

    public void OnKidsTooltipClicked()
    {
        TeleportPlayer(kidsRoom);
    }

    public void OnUserRoomTooltipClicked()
    {
        TeleportPlayer(userRoom);
    }

    public void OnForeignFilmsTooltipClicked()
    {
        TeleportPlayer(foreignFilmsRoom);
    }

    public void OnRomanticTooltipClicked()
    {
        TeleportPlayer(romanticRoom);
    }

    public void OnSciFiTooltipClicked()
    {
        TeleportPlayer(scifiRoom);
    }

    public void OnClassicsTooltipClicked()
    {
        TeleportPlayer(classicsRoom);
    }

    public void OnMisteryTooltipClicked()
    {
        TeleportPlayer(mysteryRoom);
    }


    public void TeleportPlayer(Transform targetRoom)
    {


        if (teleportationProvider == null)
        {
            Debug.LogError("[Filter me]: TeleportationProvider is not set.");
            return;
        }

        Debug.Log("[Filter me]: Target room Position " + targetRoom.position);

        Vector3 targetPosition = targetRoom.position;
        targetPosition.y -= 0.6f; //Magic number obtained through debugging. Not sure why there's a constant offset of 0.6f;

        TeleportRequest request = new TeleportRequest()
        {
            
            destinationPosition = targetPosition,
        };

        teleportationProvider.QueueTeleportRequest(request);
    }

}
