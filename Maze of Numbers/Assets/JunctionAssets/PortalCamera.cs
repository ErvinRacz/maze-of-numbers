using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    private Transform playerCamera;
    public Transform exitPortal;
    public Transform entrancePortal;

    // Update is called once per frame
    void LateUpdate()
    {
        Quaternion q = Quaternion.FromToRotation(entrancePortal.transform.right, exitPortal.transform.right);
        Vector3 playerTransformRelativeToEntrancePortal = exitPortal.position + q * (entrancePortal.position - playerCamera.position);
        playerTransformRelativeToEntrancePortal.y = playerCamera.position.y;
        transform.position = playerTransformRelativeToEntrancePortal;

        q = Quaternion.FromToRotation(entrancePortal.transform.right, -exitPortal.transform.right);
        Vector3 newCameraDirection = q * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }

    public void SetPlayerCamera(Transform tr)
    {
        playerCamera = tr; 
    }
}
