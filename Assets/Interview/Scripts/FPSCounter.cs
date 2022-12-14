using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// FPS Counting TaiLib
/// </summary>
public class FPSCounter : MonoBehaviour
{
    public Text fpsText;
    private float deltaTime;
    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS: " + Mathf.CeilToInt(fps).ToString();
        
    }
}
