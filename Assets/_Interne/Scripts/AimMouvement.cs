using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC; //utilisation de l'extension OSC pour communiquer avec TouchDesigner
using UnityEngine.SceneManagement;


public class AimMouvement : MonoBehaviour
{   
    //les variables ayant les noms associés aux variables envoyés par TouchDesigner
    public string xAddress = "/teteHorizontal";
    public string yAddress = "/teteVertical";
    public string messageJoueur = "/joueurDetecte";

    //variable nécéssaire pour recevoir du OSC de touchdesigner + la composante du crosshair dans la scene le faire bouger!
    public OSCReceiver posReceiver; //recepteur OSC
    RectTransform UI_Element; //crosshair


    private float x;
    private float y;
    private bool joueurDetecte;
    private GameManager gameManager;
    private int fctionAppeler = 0;
    private float secondeDappel;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        UI_Element = GameObject.Find("Crosshair").GetComponent<RectTransform>(); //va chercher la composante RectTransform et met le dans la varible declarer plus haut
        secondeDappel = gameManager.tempsDappelDialogueIntro;

        //lorsque le recepteur OSC recoit une valeur des variables demander, appel leur fonction
        posReceiver.Bind(xAddress, ReceiveMessageX);
        posReceiver.Bind(yAddress, ReceiveMessageY);
        posReceiver.Bind(messageJoueur, JoueurDetecte);




        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (SceneManager.GetActiveScene().buildIndex == 1)
        {

            UI_Element.anchoredPosition = new Vector3(x, y, 0f);
            

        }else if(joueurDetecte && SceneManager.GetActiveScene().buildIndex == 0 && fctionAppeler<1)
        {
            fctionAppeler++;
            Invoke("DialogueIntro", secondeDappel);
            
        }
        
        

       

    }

    void JoueurDetecte(OSCMessage message)
    {
        if (message.Values[0].FloatValue > 0)
        {
            
            joueurDetecte = true;
        }
        else if(message.Values[0].FloatValue == 0)
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


    void DialogueIntro() {
        gameManager.audioManager.DialogueIntro();
    }

}