using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasController : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas secondaryCanvas;
    void Start()
    {
        mainCanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
