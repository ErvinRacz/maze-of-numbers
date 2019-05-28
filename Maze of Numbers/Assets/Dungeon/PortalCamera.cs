using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    private Transform playerCamera;
    public Transform exitPortal;
    public Transform entrancePortal;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - entrancePortal.position;
        transform.position = exitPortal.position + playerOffsetFromPortal;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(exitPortal.rotation, entrancePortal.rotation);

        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward * -1;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }

    public void SetPlayerCamera(Transform tr)
    {
        playerCamera = tr; 
    }
}
