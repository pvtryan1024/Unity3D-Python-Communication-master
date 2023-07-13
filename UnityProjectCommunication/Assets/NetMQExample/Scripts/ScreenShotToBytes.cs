using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ScreenShotToBytes : MonoBehaviour
{
    public static byte[] bytes;
    Camera snapCam;
    public RenderTexture renderTexture;
    int resWidth = 640;
    int resHeight = 640;
    // Start is called before the first frame update

    private void Awake()
    {
        if(!snapCam) snapCam = GetComponent<Camera>();
    
        if(!renderTexture)
        {
            renderTexture = new RenderTexture(resWidth, resHeight , 24 , RenderTextureFormat.ARGB32);
            renderTexture.useMipMap = false;
            renderTexture.antiAliasing =1;
        }
    }     
    IEnumerator BlitFrameToByteArray(){
        RenderTexture.active = renderTexture;
        snapCam.targetTexture = renderTexture;
        // Debug.Log(renderTexture + " is rendertexture present?");/
        // Debug.Log(RenderTexture.active);
     //this part exists now in 2018.2 on many platforms: 
        AsyncGPUReadbackRequest request = AsyncGPUReadback.Request (renderTexture, 0, TextureFormat.RGBA32);
        while (!request.done)
        {
            yield return new WaitForEndOfFrame ();
        }
        bytes = request.GetData<byte>().ToArray ();
        Debug.Log("byte array created : " + bytes);

    }


    void Start()
    {
        snapCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        StartCoroutine("BlitFrameToByteArray");
    }
}
