using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{   
    
    //Manager UI et BUT
    [SerializeField] private float tempsExperienceMinute;
    [SerializeField] private int vies = 3;
    [SerializeField] private string textGagne = "Vous avez gagné!";
    [SerializeField] private string textPerdu = "Vous êtes mort!";

    //Manager Ennemi
    [SerializeField] private int tempsPremierSpawnEnnemiSeconde = 50;
    [SerializeField] private GameObject[] ennemis;
    [SerializeField] private int rapiditeEnnemi = 50;


    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform canon;


    //Manager les Manager
    [SerializeField] private AudioManager audioManager;


    //Manager temporaire
    [SerializeField] private TextMeshProUGUI textMilieu;


    private GameObject ennemiTarget;

    public bool vivant = true;

    // Start is called before the first frame update
    void Start()
    {
        ennemiTarget = GameObject.Find("HitTarget");
        tempsExperienceMinute *= 60;
        Invoke("Gagne", tempsExperienceMinute);
        Invoke("SpawnEnnemi", tempsPremierSpawnEnnemiSeconde);
        
    }

    // Update is called once per frame
    void Update()
    {
        VerifMort();
        

    }

    void FixedUpdate()
    {
        BougeCanonAvecAim();
        
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

    public void SpawnEnnemi()
    {
        if (vivant)
        {
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
            ennemi.transform.LookAt(mainCamera.transform.position,Vector3.up);

        


            Invoke("SpawnEnnemi", (float)Random.Range(3, 8));


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
        if (vivant)
        {
            textMilieu.text = textGagne;
            textMilieu.gameObject.SetActive(true);
        }
        
    }

    public void PerdreVie()
    {
        vies--;
    }
}
