using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    public bool showDebug;

    void Awake()
    {
        
    }

    public void GenerateNewMaze(int sizeRows, int sizeCols)
    {
        // stub to fill in
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
}
