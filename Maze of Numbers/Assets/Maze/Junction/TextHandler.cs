using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject text = new GameObject();
        TextMesh t = text.AddComponent<TextMesh>();
        t.anchor = TextAnchor.MiddleCenter;
        t.text = "new text set";
        t.fontSize = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
