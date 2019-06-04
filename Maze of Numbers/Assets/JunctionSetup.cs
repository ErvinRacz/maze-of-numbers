using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(CharacterController))]
public class JunctionSetup : MonoBehaviour
{
    public GameObject playerCamera;
    public CharacterController player;

    private void Awake()
    {
        Camera[] cameras = GetComponentsInChildren<Camera>();
        PortalTeleporterScript[] tpScripts = GetComponentsInChildren<PortalTeleporterScript>();
        TextMesh[] textMeshes = GetComponentsInChildren<TextMesh>(); 

        foreach (var camera in cameras)
        {
            PortalCamera pcScript = camera.GetComponent<PortalCamera>();
            pcScript.SetPlayerCamera(playerCamera.transform);
        }

        foreach (var tpScript in tpScripts)
        {
            tpScript.setPlayerBody(player);
        }

        string number = name.Split('#')[1];

        foreach (var textMesh in textMeshes)
        {
            textMesh.text = number;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
