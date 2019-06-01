using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporterScript : MonoBehaviour
{
    private Transform player;
    public Transform reciever;

    private bool playerIsOverlapping = false;

    // Update is called once per frame

    void Update()
    {
        if (playerIsOverlapping)
        {
            //Vector3 portalToPlayer = player.position - transform.position;
            Vector3 forward = player.TransformDirection(Vector3.forward);
            Vector3 toPortal =  player.position - transform.position;

            float dotProduct = Vector3.Dot(forward, toPortal);
            if (dotProduct < 0f)
            {
                print("The other transform is behind me!");
                // Teleport him!
                Quaternion q = Quaternion.FromToRotation(transform.right, player.right);
                Vector3 playerTransformRelativeToEntrancePortal = reciever.position + q * (transform.position - player.position);
                playerTransformRelativeToEntrancePortal.y = player.position.y;
                player.position = playerTransformRelativeToEntrancePortal;

                q = Quaternion.FromToRotation(transform.right, -reciever.right);
                Vector3 newCameraDirection = q * player.forward;
                player.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);

                playerIsOverlapping = false;
            }
        }
    }

    public void setPlayerBody(Transform player)
    {
        this.player = player;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("enter");
            playerIsOverlapping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("exit");
            playerIsOverlapping = false;
        }
    }
}
