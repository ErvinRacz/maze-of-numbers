using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]
public class GameController : MonoBehaviour
{
    private MazeConstructor generator;

    // Start is called before the first frame update
    void Start()
    {
        generator = GetComponent<MazeConstructor>(); 
    }
}
