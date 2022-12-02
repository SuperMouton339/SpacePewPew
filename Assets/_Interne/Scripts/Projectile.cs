using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject explosionMissile;

    void Start()
    {
        Invoke("Destroy", 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().FairePew(gameObject.GetComponent<AudioSource>());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if( collision.gameObject.tag == "ennemi")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        
        
    }

    private void Destroy()
    {
        
        Destroy(gameObject);
        
    }


    private void OnDestroy()
    {
        if(explosionMissile != null)
        {
            
            Instantiate(explosionMissile, gameObject.transform.position, Quaternion.identity);
        }
    }
}
