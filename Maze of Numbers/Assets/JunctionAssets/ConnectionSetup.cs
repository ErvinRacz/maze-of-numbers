using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionSetup : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject from;
    public GameObject destionation;

    private RendererSetup rs;
    private PortalTeleporterScript portalTeleporterScript;
    private PortalCamera portalCamera;

    void Awake()
    {
        rs = this.GetComponentInChildren<RendererSetup>();
        rs.camera = destionation.GetComponentInChildren<Camera>();

        portalTeleporterScript = this.GetComponentInChildren<PortalTeleporterScript>();
        portalTeleporterScript.reciever = destionation.transform.Find("RenderPlane");

        portalCamera = this.GetComponentInChildren<PortalCamera>();
        portalCamera.inPlaneTr = from.transform.Find("RenderPlane");
    }
}
