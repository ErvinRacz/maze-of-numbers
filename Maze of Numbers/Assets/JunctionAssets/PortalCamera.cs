using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    private Transform playerCamera;
    private Transform outPlaneTr;
    public Transform inPlaneTr;

    // Update is called once per frame

    private void Start()
    {
        outPlaneTr = transform.parent.Find("RenderPlane");
        var camera = GetComponent<Camera>();
        camera.enabled = false;
    }

    void LateUpdate()
    {
        Quaternion q = Quaternion.FromToRotation(inPlaneTr.transform.right, outPlaneTr.transform.right);
        Vector3 playerTransformRelativeToEntrancePortal = outPlaneTr.position + q * (inPlaneTr.position - playerCamera.position);
        playerTransformRelativeToEntrancePortal.y = playerCamera.position.y;
        transform.position = playerTransformRelativeToEntrancePortal;

        Vector3 newCameraDirection = q * playerCamera.forward;
        newCameraDirection.y = -newCameraDirection.y;
        transform.rotation = Quaternion.LookRotation(-newCameraDirection, Vector3.up);
    }

    public void SetPlayerCamera(Transform tr)
    {
        playerCamera = tr; 
    }
}
