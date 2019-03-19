using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilder : MonoBehaviour
{
    public GameObject junction;
    public int length = 100;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate();
    }

    private void Instantiate()
    {
        for (int z = 0; z < 10; z++)
        {
            for (int x = 0; x < 10; x++)
            {
                Instantiate(junction, new Vector3(x*12, 0, z*12), Quaternion.identity);
            }
        }
    }
}
