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
    RectTransform UI_Element;
    private float x;
    private float y;


    

    // Start is called before the first frame update
    void Start()
    {
   
        UI_Element = GameObject.Find("Crosshair").GetComponent<RectTransform>();
        posReceiver.Bind(xAddress, ReceiveMessageX);
        posReceiver.Bind(yAddress, ReceiveMessageY);

        
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("X:" + x);
        UI_Element.anchoredPosition = new Vector3(x, y, 0f);
    }
    void ReceiveMessageX(OSCMessage message)
    {
        x = message.Values[0].FloatValue;
        
    }
    void ReceiveMessageY(OSCMessage message)
    {
        y = message.Values[0].FloatValue;
        
    }


}

