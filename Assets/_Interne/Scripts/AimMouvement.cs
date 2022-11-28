using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;
using UnityEngine.SceneManagement;


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
    private GameManager gameManager;
    private int fctionAppeler = 0;
    private float secondeDappel;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UI_Element = GameObject.Find("Crosshair").GetComponent<RectTransform>();
        secondeDappel = gameManager.tempsDappelDialogueIntro;
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