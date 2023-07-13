using System;
using System.Collections;
using System.Collections.Generic;
using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;



public class VideoStreamReader : MonoBehaviour
{
    public Camera camera ;
    public static string asBase64String;
    public int imgHeight = 640;
    public int imgWidht = 640;
    // Start is called before the first frame update
    void Awake()
    {
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        VideoStream();
        Debug.Log(asBase64String);
    }

    public void VideoStream(){
        RenderTexture rt = new RenderTexture(imgWidht, imgHeight, 24);
        camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(imgWidht, imgHeight, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, imgWidht, imgHeight), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        rt.Release();
        byte[] asBytes = screenShot.EncodeToPNG();

        string asBase64String = Convert.ToBase64String(asBytes);

    }
}
