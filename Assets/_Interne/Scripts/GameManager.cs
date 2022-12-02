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
    [SerializeField] public float tempsDappelDialogueIntro = 10;

    //[SerializeField] private int vies = 3;
    [SerializeField] private GameObject[] listeVies;
    [SerializeField] private GameObject mort;
    [SerializeField] private Slider sliderProgression;
    [SerializeField] private GameObject sliderObject;
    [SerializeField] private GameObject listeViesObject;

    //Manager Ennemi
    //[SerializeField] private int tempsPremierSpawnEnnemiSeconde = 50;
    [SerializeField] private GameObject[] ennemis;
    
    [SerializeField] private int rapiditeEnnemi = 35;
    [SerializeField] private int limiteEnnemi = 3;//limite d'ennemi sur scene
    public int compteurEnnemis ; //quantite ennemis sur la scene

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform canon;


    //Manager les Manager
    [SerializeField] public AudioManager audioManager;

    //Animations
    [SerializeField] private Animator fadeAnim;


    private GameObject ennemiTarget;

    public bool vivant = true;
    public bool gagne = false;
    public bool joueurDetecte = false;


    // Start is called before the first frame update
    void Start()
    {
        ennemiTarget = GameObject.Find("HitTarget");
        tempsExperienceMinute *= 60;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //audioManager.DialogueIntro(); //faire jouer le dialogue d'intro
            fadeAnim.Play("FonduNoir");
        }

        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Invoke("SpawnEnnemi", (float)Random.Range(3, 8));
            Invoke("IntroGame", 1.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        

    }

    void FixedUpdate()
    {
        if (vivant && !gagne && SceneManager.GetActiveScene().buildIndex == 1)
        {
            ProgressBar();
            BougeCanonAvecAim();
            //VerifEnnemi();
        }

        else
        {
            rapiditeEnnemi = 0;
        }
        
        
    }

    public void IntroGame()
    {
        sliderObject.SetActive(true);
        listeViesObject.SetActive(true);
        fadeAnim.Play("DebutJeuUX");
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
            int x = Random.Range(-450, 450);
            int y = Random.Range(-250, 250);
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
   

    private void Gagne()
    {
        audioManager.DialogueGagne();
    }
    
    private void Perdre()
    {
        rapiditeEnnemi = 0;
        audioManager.DialoguePerdu();
        fadeAnim.Play("FonduNoir 2");
        mort.gameObject.SetActive(true);
        vivant = false;
    }

    public void PerdreVie()
    {
        for (int i = 0; i < listeVies.Length; i++) //Boucle parcourant la grandeur du tableau listeVies
        {
            //listeVies[i].GetComponent<image>().sprite == variableAvecImage si on vx utiliser un changement d'image
            if (listeVies[i].activeInHierarchy == true) //si l'élément se trouvant dans le tableau listeVies à l'index i EST activer dans la hierarchy des GameObject
            {
                listeVies[i].SetActive(false); // désactiver l'élément du tableau listeVies à l'index i

                if (i == listeVies.Length - 1) // si l'index i est egale a la grandeur du tableau listeVies -1
                {
                    Perdre(); //lancer la fonction GameOver
                    return;//arreter la boucle
                }
                audioManager.ImpactVaisseau(); //faire jouer la variable audio audioPerteVie lorsque le perso perd une vie
                return; //arreter la boucle
            }
        }
        
        
    }
}