using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActivator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject RenderPlane1;
    public GameObject RenderPlane2;
    public GameObject RenderPlane3;

    private Camera[] cameras = new Camera[3]; // array of cameras that have to be rendered

    void Start()
    {
        cameras[0] = RenderPlane1.GetComponent<RendererSetup>().camera;
        cameras[1] = RenderPlane2.GetComponent<RendererSetup>().camera;
        cameras[2] = RenderPlane3.GetComponent<RendererSetup>().camera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            for(int i = 0; i < 3; i++)
            {
                cameras[i].enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            for (int i = 0; i < 3; i++)
            {
                // necessary to turn off for optimising the performance
                cameras[i].enabled = false;
            }
        }
    }
}
