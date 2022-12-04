using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech; //implementation de cortana
using TMPro;


public class PewPew : MonoBehaviour
{
    //variable speech
    private KeywordRecognizer keywordRecognizer; //variable qui permet a cortana d'�couter et de d�tecter les mots cl�s
    private Dictionary<string, Action> actions = new Dictionary<string, Action>(); //variable tableau avec les mots cl�s (Dictionnaire)


    //le missile
    [SerializeField] private GameObject projectiles;



    [SerializeField] private GameObject crosshair;


    //rapidit� du projectile
    [SerializeField] private float shootForce; //force du projectile
    [SerializeField] private float upwardForce;

    //stats du canon
    [SerializeField] private float timeBetweenShooting;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float shootTime;
    /*
    [SerializeField] private int missilesQty; //si on veut rajouter une quantit� limit� de missile et faire un reload
    [SerializeField] private float reloadTime; //le temps de recharge du canon

    */
    private int missilesLeft;
    private int missilesShot;
    //les BOOLS
    public bool shooting; //variable qui te permet de shoot
   // private bool readyToShoot;
    //private bool reloading;

    //REFERENCE
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Camera mainCamera;


    //Graphics

    [SerializeField] private TextMeshProUGUI ammunitionDisplay;

    //Pour fixer des bug
    public bool allowInvoke = true;


    //les manager
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioManager audioManager;

    // Start is called before the first frame update
    private void Awake()
    {
        /*mettre la variable de missile qui reste a full capacit�
         
        missilesLeft = missilesQty;
        

        */
        //readyToShoot = true;
    }


    // Start is called before the first frame update
    void Start()
    {   
        //ajout des mots pew, pew pew, piou et piou piou dans le dictionnaires des mots cl� � d�tecter
        actions.Add("pew", Pew); 
        actions.Add("pew pew", Pew);
        actions.Add("piou", Pew);
        actions.Add("piou piou", Pew);
        
        

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray()); //ajout du dictionnaire complet dans la variable keywordRecognizer
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech; //d�s qu'elle entend des combinaisons de mots de la variable keywordRecognizer, elle lance la m�thode RecognizedSpeech
        keywordRecognizer.Start(); //on instantie cortana pour qu'elle �coute
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //si le UI de munition existe
        if(ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(missilesLeft + " / "+ missilesQty);
        }
        */
    }

    

    //Fonctions de speech
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        //lorsque reconnue, lance la fonction associ� au mot cl�. La m�thode �tant Pew!! pour tir� le missile!!!
        actions[speech.text].Invoke();

    }
    private void Pew()
    {
        
        VerifShoot();
    }
    
    
    /*Fonctions pour appeler le reload
     * 
     * if(Le input decider pour reload est true  && !reloading && missileLeft < missileQty) 
     * {
     * Reload();
     * 
     * }
     * 
     * if(readyToShoot && shooting && !reloading && missileLeft <= 0)
     * {
     *      Reload();
     * }
     * 
     */




    
    
    //fonctions pour tir�
    public void VerifShoot()
    {
        //condition pour shoot
        /*if (readyToShoot) //&& !reloading && missilesLeft>0
        {
            */

            

            //missilesShot = 0;   
            Shoot();
        //}
    }
    private void Shoot()
    {
        


        //readyToShoot = false;

        //trouver la position exact de l'impact avec raycast
        Ray rayOrigin = Camera.main.ScreenPointToRay(crosshair.transform.position);
        RaycastHit hitInfo;

        //regarder si le ray touche quelque chose
        Vector3 hitPoint;
            if (Physics.Raycast(rayOrigin, out hitInfo)) 
            { 
                hitPoint = hitInfo.point; 
            }
            else
            {
                hitPoint = rayOrigin.GetPoint(75); //un point plus loin de la camera
            }
         //calculer la direction du missile
         Vector3 directionMissile = hitPoint - attackPoint.position;

        //faire instancier le missile
        GameObject currentMissile = Instantiate(projectiles, attackPoint.position, Quaternion.identity);
        //tourner le missile dans la direction tir�
        currentMissile.transform.forward = directionMissile.normalized;

        


        //ajouter de la force au missile
        currentMissile.GetComponent<Rigidbody>().AddForce(directionMissile.normalized * shootForce, ForceMode.Impulse);


        //missilesLeft--;
        //missilesShot++;

        //le temps avant de tir� le prochain missile
        /*if (allowInvoke)
        {
            ResetShot();
            allowInvoke = false;
        }*/
    }


    /*private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }*/

    // Si on veut mettre un reload
    /*
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        missilesLeft = missilesQty;
        reloading = false;
    }
    */

}
