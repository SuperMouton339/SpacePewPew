using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementEnnemi : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject animationMort;
    private GameObject target;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        target = GameObject.Find("MainCamera");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision != null && collision.gameObject == target)
        {
            gameManager.PerdreVie();

        }
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        Instantiate(animationMort);
    }
}
