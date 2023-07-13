using UnityEngine;
using NetMQ;

public class HelloClient : MonoBehaviour
{
    // private HelloRequester _helloRequester;
    private PhotoSender _photoSender;
    private ScreenShotToBytes _screenToBytes;
    
    private void Start()
    {
        // _helloRequester = new HelloRequester();
        _photoSender = new PhotoSender();
        _screenToBytes = new ScreenShotToBytes();
        // _helloRequester.Start();
        _photoSender.Start();
    }

    public void Update(){
        
    }

    private void OnDestroy()
    {
        // _helloRequester.Stop();
        _photoSender.Stop();
        NetMQConfig.Cleanup();
    }
}