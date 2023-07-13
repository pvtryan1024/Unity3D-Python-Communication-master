using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;

/// <summary>
///     Example of requester who only sends Hello. Very nice guy.
///     You can copy this class and modify Run() to suits your needs.
///     To use this class, you just instantiate, call Start() when you want to start and Stop() when you want to stop.
/// </summary>
public class PhotoSender : RunAbleThread
{
    bool byteArrayCreated = false;
    /// <summary>
    ///     Request Hello message to server and receive message back. Do it 10 times.
    ///     Stop requesting when Running=false.
    /// </summary>
    protected override void Run()
    {
        ForceDotNet.Force(); // this line is needed to prevent unity freeze after one use, not sure why yet
        using (RequestSocket client = new RequestSocket())
        {
            client.Connect("tcp://localhost:5555");
            
            while (Running)
            // for (int i = 0; i < 10 && Running; i++)
            {
                int iterationCount = 0;
                Debug.Log("Sending Hello");
                // client.SendFrame("Hello");

                if(ScreenShotToBytes.bytes == null){
                    Debug.Log("byte array is empty");
                    byteArrayCreated = false;
                    // client.SendFrame("ImageFile Not Sent");
                }
                else{
                    byte[] byteArray= ScreenShotToBytes.bytes;
                    Debug.Log("byteArray ready to be sent");
                    byteArrayCreated = true;
                    client.SendFrame(byteArray);
                }

                // ReceiveFrameString() blocks the thread until you receive the string, but TryReceiveFrameString()
                // do not block the thread, you can try commenting one and see what the other does, try to reason why
                // unity freezes when you use ReceiveFrameString() and play and stop the scene without running the server
//                string message = client.ReceiveFrameString();
//                Debug.Log("Received: " + message);
                string message = null;
                bool gotMessage = false;
                while (Running && byteArrayCreated)
                {
                    gotMessage = client.TryReceiveFrameString(out message); // this returns true if it's successful
                    if (gotMessage) {

                        break;
                    }
                    else{
                        Debug.Log("Awaiting Message");
                    }
                    Debug.Log("iteration count num : " + iterationCount);
                }

                if (gotMessage) Debug.Log("Received " + message);
                iterationCount++;
            }
        }

        NetMQConfig.Cleanup(); // this line is needed to prevent unity freeze after one use, not sure why yet
    }
}