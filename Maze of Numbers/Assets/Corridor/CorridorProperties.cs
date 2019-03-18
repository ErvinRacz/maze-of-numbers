using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[RequireComponent(typeof(Material))]
public class CorridorProperties : MonoBehaviour
{
    [SerializeField] private float length = 4.0f;
    [SerializeField] private float thickness = 0.2f;
    [SerializeField] private float width = 2.0f;


    private Component[] components;
    [SerializeField] private Material corridorBaseMaterial;

    private void Awake()
    {
        //UpdateCorridorParameters();
    }

    void Start()
    {
        //UpdateCorridorParameters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        UpdateCorridorParameters();
    }

    private void UpdateCorridorParameters()
    {
        if (EditorApplication.isPlaying) return;

        foreach (Transform child in transform)
        {
            child.localScale = new Vector3(length, thickness, width);
            child.gameObject.GetComponent<MeshRenderer>().material = corridorBaseMaterial;
        }
    }
}
