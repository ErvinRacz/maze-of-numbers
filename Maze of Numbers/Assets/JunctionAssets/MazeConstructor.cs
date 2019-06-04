using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    public bool showDebug;

    public GameObject junctionPrefab;
    public GameObject playerCamera;
    public CharacterController player;

    public void GenerateNewMaze(int size, TriggerEventHandler startCallback = null, TriggerEventHandler goalCallback = null)
    {
        GameObject currentJunction;
        Camera[] cameras;
        PortalTeleporterScript[] tpScripts;
        for(int i = 0; i < size; i++)
        {
            currentJunction = Instantiate(junctionPrefab, new Vector3(55 * i, -5, 0), Quaternion.identity);
            cameras = currentJunction.GetComponentsInChildren<Camera>();
            tpScripts = currentJunction.GetComponentsInChildren<PortalTeleporterScript>();
            foreach (var camera in cameras)
            {
                PortalCamera pcScript = camera.GetComponent<PortalCamera>();
                pcScript.SetPlayerCamera(playerCamera.transform);
            }

            foreach (var tpScript in tpScripts)
            {
                tpScript.setPlayerBody(player);
            }
        }
    }
}
