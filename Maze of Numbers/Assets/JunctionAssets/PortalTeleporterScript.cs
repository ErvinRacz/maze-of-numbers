using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporterScript : MonoBehaviour
{
    private GameObject player;
    private PlayerMove playerMoveScript;
    public Transform reciever;

    private bool playerIsOverlapping = false;
    int updateToSkipAmount = 0;
    Vector3 playerTransformRelativeToEntrancePortal;
    bool inPortal = false;

    private void Start()
    {
        playerMoveScript = player.GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if(updateToSkipAmount == 3)
        {
            player.transform.position = playerTransformRelativeToEntrancePortal;
        }
    }

    private void LateUpdate()
    {
        if (updateToSkipAmount > 0) { updateToSkipAmount--;  return; }
        if (!inPortal) return;

        Vector3 vecToCurrentPosition = player.transform.position - transform.position;
        Vector3 vecToPreviousPosition = playerMoveScript.PreviousPosition - transform.position;

        // Rough distance thresholds we must be within to teleport
        float sideDistance = Vector3.Dot(transform.right, vecToCurrentPosition);
        float frontDistance = Vector3.Dot(transform.up, vecToCurrentPosition); // UP because the plane has been rotated by 90 deg around the x axis.
        float heightDistance = Vector3.Dot(transform.forward, vecToCurrentPosition); // 
        float previousFrontDistance = Vector3.Dot(transform.up, vecToPreviousPosition);

        // Have we just crossed the portal threshold
        if (frontDistance < 0.0f
            && previousFrontDistance >= 0.0f
            && Mathf.Abs(sideDistance) < /*approx portal_width*/ 7f
            && Mathf.Abs(heightDistance) < /*approx portal_height*/ 7f)
        {
            // Teleport him!
            Quaternion q = Quaternion.FromToRotation(transform.right, reciever.right);
            playerTransformRelativeToEntrancePortal = reciever.position + q * (transform.position - player.transform.position);
            playerTransformRelativeToEntrancePortal.y = player.transform.position.y;
            player.transform.position = playerTransformRelativeToEntrancePortal;

            q = Quaternion.FromToRotation(transform.right, -reciever.right);
            Vector3 newCameraDirection = q * player.transform.forward;
            player.transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);

            // Since, the player is not teleported to the desired position right after this code 
            // gets to execute, we have to ensure, that we do not modify the values that were calculated once correctly.
            updateToSkipAmount = 3;
            inPortal = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            inPortal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            inPortal = false;
        }
    }

    public void setPlayerBody(GameObject player)
    {
        this.player = player;
    }
}
