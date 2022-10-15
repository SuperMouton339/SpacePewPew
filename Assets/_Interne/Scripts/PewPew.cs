using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;


public class PewPew : MonoBehaviour
{
    //variable speech
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();


    //le missile
    [SerializeField] private GameObject projectiles;



    [SerializeField] private GameObject crosshair;


    //rapidité du projectile
    [SerializeField] private float shootForce;
    [SerializeField] private float shootSpeed;

    //stats du canon
    [SerializeField] private float shootTime;

    [SerializeField] private int missilesQty; //si on veut rajouter une quantité limité de missile et faire un reload
    [SerializeField] private float reloadTime; //le temps de recharge du canon
    private int missilesLeft;
    private int missilesShot;
    //les BOOLS
    public bool shooting; //variable qui te permet de shoot
    private bool readyToShoot;
    private bool reloading;

    //REFERENCE
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Camera mainCamera;


    // Start is called before the first frame update
    private void Awake()
    {
        /*mettre la variable de missile qui reste a full capacité
         
        missilesLeft = missilesQty;
        

        */
        readyToShoot = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        actions.Add("pew", Pew);
        

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    //Fonctions de speech
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        actions[speech.text].Invoke();
    }
    private void Pew()
    {
        Debug.Log("Je Pew");
        VerifShoot();
    }
    
    
    
    
    //fonctions pour tiré
    public void VerifShoot()
    {
        //condition pour shoot
        if (readyToShoot) //&& !reloading && missilesLeft>0
        {
            //missilesShot = 0;   
            Shoot();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //missilesLeft--;
        //missilesShot++;

        //faire instancier le missile
    }
}
