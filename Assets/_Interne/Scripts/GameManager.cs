using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Manager UI et BUT
    private float progressionTimer = 0;
    [SerializeField] private float tempsExperienceMinute;
    [SerializeField] private int vies = 3;
    [SerializeField] private string textGagne = "Vous avez gagné!";
    [SerializeField] private string textPerdu = "Vous êtes mort!";
    [SerializeField] private Slider sliderProgression;

    //Manager Ennemi
    [SerializeField] private int tempsPremierSpawnEnnemiSeconde = 50;
    [SerializeField] private GameObject[] ennemis;
    
    [SerializeField] private int rapiditeEnnemi = 50;
    [SerializeField] private int limiteEnnemi = 3;//limite d'ennemi sur scene
    public int compteurEnnemis ; //quantite ennemis sur la scene

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform canon;


    //Manager les Manager
    [SerializeField] private AudioManager audioManager;


    //Manager temporaire
    [SerializeField] private TextMeshProUGUI textMilieu;


    private GameObject ennemiTarget;

    public bool vivant = true;
    public bool gagne = false;

    // Start is called before the first frame update
    void Start()
    {
        ennemiTarget = GameObject.Find("HitTarget");
        tempsExperienceMinute *= 60;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Invoke("SpawnEnnemi", (float)Random.Range(3, 8));
        }
    }

    // Update is called once per frame
    void Update()
    {
        VerifMort();
        

    }

    void FixedUpdate()
    {
        if (vivant && !gagne)
        {
        ProgressBar();
        BougeCanonAvecAim();
        VerifEnnemi();
        }
        
        
    }

    //Fonction pour faire Bouger le canon selon le AIM
    private void BougeCanonAvecAim()
    {
        Ray rayOrigin = Camera.main.ScreenPointToRay(GameObject.Find("Crosshair").transform.position);
        RaycastHit hitInfo;


        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Vector3 direction;
            if (hitInfo.collider != null)
            {
                //Diraction = Destination(hitInfo.point) - source (canon)
                direction = hitInfo.point - canon.position;

                canon.rotation = Quaternion.LookRotation(direction);
            }
            else
            {
                direction = rayOrigin.GetPoint(75) - canon.position;
                canon.rotation = Quaternion.LookRotation(direction);
            }
        }
    }

    public void VerifEnnemi()
    {
            
            
            

        


            


        
        
        
    }
    public void SpawnEnnemi()
    {       
        if (compteurEnnemis < limiteEnnemi)
        {   
            compteurEnnemis++;
            int x = Random.Range(-600, 600);
            int y = Random.Range(-400, 400);
            int z = 700;
            
            //Vector3 targetDirection = new Vector3(ennemiTarget.transform.position.x, ennemiTarget.transform.position.y, ennemiTarget.transform.position.z);
            Vector3 spawnPosition = new Vector3(x, y, z);

            Vector3 targetDirection = ennemiTarget.transform.position - spawnPosition;
            int ennemiAleatoire = Random.Range(0, ennemis.Length);
            GameObject ennemi = Instantiate(ennemis[ennemiAleatoire], spawnPosition, ennemis[ennemiAleatoire].transform.rotation);
            //direction
            ennemi.transform.forward = targetDirection.normalized;
            //deplacement
            ennemi.GetComponent<Rigidbody>().AddForce(targetDirection.normalized * rapiditeEnnemi, ForceMode.Impulse);
            //regard
            ennemi.transform.LookAt(mainCamera.transform.position, Vector3.up);

            Invoke("SpawnEnnemi", (float)Random.Range(3, 8));
        }
        else
        {
            Invoke("SpawnEnnemi", (float)Random.Range(3, 8));
        }
    }



    //progress bar en fonction du timer donner
    private void ProgressBar()
    {
        
        progressionTimer += Time.deltaTime;
        sliderProgression.value = ( progressionTimer / tempsExperienceMinute);
        if(progressionTimer >= tempsExperienceMinute)
        {
            gagne = true;
            Gagne();
        }
        
        
    }



    //Fonctions pour Gagne ou perdre/Mourrir
    private void VerifMort()
    {
        if (vies <= 0)
        {
            vivant = false;
            textMilieu.text = textPerdu;
            textMilieu.gameObject.SetActive(true);
        }
    }

    private void Gagne()
    {
        
            textMilieu.text = textGagne;
            textMilieu.gameObject.SetActive(true);
            
        
        
    }

    public void PerdreVie()
    {
        vies--;
        audioManager.ImpactVaisseau();
    }
}