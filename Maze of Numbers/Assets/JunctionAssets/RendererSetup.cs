using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererSetup : MonoBehaviour
{

    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        if (camera.targetTexture != null)
        {
            camera.targetTexture.Release();
        }

        Renderer rend = GetComponent<Renderer>();

        camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        Material mat = new Material(Shader.Find("Unlit/ScreenCutoutShader"));
        mat.mainTexture = camera.targetTexture;

        rend.material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
