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

    [SerializeField] private Material corridorBaseMaterial;
    [SerializeField] private float length = 3.0f;
    [SerializeField] private float thickness = 0.2f;
    [SerializeField] private float width = 3f;

    private Component[] components;

    private void Awake()
    {
        //UpdateCorridorParameters();
    }

#if UNITY_EDITOR
    void Start()
    {
        UpdateCorridorParameters();
    }
#endif

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCorridorParameters()
    {
        if (EditorApplication.isPlaying) return;

        foreach (Transform child in transform)
        {
            child.localScale = new Vector3(length, thickness, width);
            //child.gameObject.GetComponent<MeshRenderer>().material = corridorBaseMaterial;
        }
    }
}
