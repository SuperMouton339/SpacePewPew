using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Kinect;
using Microsoft.Kinect.Face;
using extOSC;

public class AimMouvement : MonoBehaviour
{
    public string xAddress = "/teteHorizontal";
    public string yAddress = "/teteVertical";
    
    public OSCReceiver posReceiver;

    private Vector3 crosshairPos;

    // Start is called before the first frame update
    void Start()
    {
         crosshairPos = transform.position;

        posReceiver.Bind(xAddress, ReceiveMessageX);
        posReceiver.Bind(yAddress, ReceiveMessageY);

        
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = crosshairPos;

    }
    void ReceiveMessageX(OSCMessage message)
    {
        float x = message.Values[0].FloatValue;
        
        crosshairPos.x = x;
    }
    void ReceiveMessageY(OSCMessage message)
    {
        float y = message.Values[0].FloatValue;

        crosshairPos.y = y;
       
    }
}
