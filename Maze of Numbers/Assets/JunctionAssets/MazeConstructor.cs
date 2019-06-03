using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    public bool showDebug;

    public GameObject junctionPrefab;
    public GameObject playerCamera;
    public GameObject player;

   

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

    void OnGUI()
    {
        if (!showDebug)
        {
            return;
        }

        string msg = player.transform.position + "";

        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }

    private void PlaceStartTrigger(TriggerEventHandler callback)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //go.transform.position = new Vector3(startCol * hallWidth, .5f, startRow * hallWidth); //TODO: 
        go.name = "Start Trigger";
        go.tag = "Generated";

        go.GetComponent<BoxCollider>().isTrigger = true;
        //go.GetComponent<MeshRenderer>().sharedMaterial = startMat;

        TriggerEventRouter tc = go.AddComponent<TriggerEventRouter>();
        tc.callback = callback;
    }

    private void PlaceGoalTrigger(TriggerEventHandler callback)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //go.transform.position = new Vector3(goalCol * hallWidth, .5f, goalRow * hallWidth);
        go.name = "Treasure";
        go.tag = "Generated";

        go.GetComponent<BoxCollider>().isTrigger = true;
        //go.GetComponent<MeshRenderer>().sharedMaterial = treasureMat;

        TriggerEventRouter tc = go.AddComponent<TriggerEventRouter>();
        tc.callback = callback;
    }
}
