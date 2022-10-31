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



    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform canon;


    //Manager temporaire
    [SerializeField] private TextMeshProUGUI textMilieu;

    

    
    

    // Start is called before the first frame update
    void Start()
    {
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
        int x = Random.Range(-600, 600);
        int y = Random.Range(-400, 400);
        int z = 991;
        Vector3 spawnPosition = new Vector3(x, y, z);
        int ennemiAleatoire = Random.Range(0, ennemis.Length - 1);
        Instantiate(ennemis[ennemiAleatoire], spawnPosition, ennemis[ennemiAleatoire].transform.rotation);
    }


    //Fonctions pour Gagne ou perdre/Mourrir
    private void VerifMort()
    {
        if (vies <= 0)
        {
            
            textMilieu.text = textPerdu;
            textMilieu.gameObject.SetActive(true);
        }
    }

    private void Gagne()
    {
        textMilieu.text = textGagne;
        textMilieu.gameObject.SetActive(true);
    }
}
