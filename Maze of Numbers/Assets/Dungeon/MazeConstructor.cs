using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    public bool showDebug;

    public GameObject junctionPrefab;
    public GameObject playerCamera;

    void Awake()
    {
        
    }

    public void GenerateNewMaze(int size, TriggerEventHandler startCallback = null, TriggerEventHandler goalCallback = null)
    {
        GameObject currentJunction;
        Camera[] cameras;
        for(int i = 0; i < size; i++)
        {
            currentJunction = Instantiate(junctionPrefab, new Vector3(55 * i, -5, 0), Quaternion.identity);
            cameras = currentJunction.GetComponentsInChildren<Camera>();
            foreach (var camera in cameras)
            {
                PortalCamera pcScript = camera.GetComponent<PortalCamera>();
                pcScript.SetPlayerCamera(playerCamera.transform);
            }
        }
    }

    void OnGUI()
    {
        if (!showDebug)
        {
            return;
        }

        int[,] maze = new int[,]
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
        };
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        string msg = "";

        for (int i = rMax; i >= 0; i--)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    msg += "....";
                }
                else
                {
                    msg += "==";
                }
            }
            msg += "\n";
        }

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
