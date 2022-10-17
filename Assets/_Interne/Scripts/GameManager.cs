using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{   
    //Reference
    
    [SerializeField] private Camera mainCamera;

    [SerializeField] private Transform canon;


    [SerializeField] private float tempsExperienceMinute;


    [SerializeField] private TextMeshProUGUI textMilieu;
    

    [SerializeField] private int vies = 3;
    // Update is called once per frame

    // Start is called before the first frame update
    void Start()
    {
        tempsExperienceMinute *= 60;
        Invoke("Gagne", tempsExperienceMinute);
        
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




























    //Fonctions pour Gagne ou perdre/Mourrir
    private void VerifMort()
    {
        if (vies <= 0)
        {
            
            textMilieu.text = "Vous êtes Mort!";
            textMilieu.gameObject.SetActive(true);
        }
    }

    private void Gagne()
    {
        textMilieu.text = "Vous avez Gagné!";
        textMilieu.gameObject.SetActive(true);
    }
}
