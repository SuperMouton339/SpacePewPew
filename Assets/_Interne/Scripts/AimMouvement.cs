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
    public string messageJoueur = "/joueurDetecte";

    public OSCReceiver posReceiver;
    RectTransform UI_Element;
    private float x;
    private float y;
    private bool joueurDetecte;



    // Start is called before the first frame update
    void Start()
    {

        UI_Element = GameObject.Find("Crosshair").GetComponent<RectTransform>();

        posReceiver.Bind(xAddress, ReceiveMessageX);
        posReceiver.Bind(yAddress, ReceiveMessageY);
        //posReceiver.Bind(messageJoueur, JoueurDetecte);




        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {


        //Debug.Log("Je detecte le joueur");
        UI_Element.anchoredPosition = new Vector3(x, y, 0f);

        /*else
        {
            Debug.Log("Je ne detecte pas le joueur");
            transform.position = Input.mousePosition;
        }*/

    }

    void JoueurDetecte(OSCMessage message)
    {
        if (message.Values[0].FloatValue > 0)
        {
            joueurDetecte = true;
        }
        else
        {
            joueurDetecte = false;
        }

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