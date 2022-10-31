using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementEnnemi : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private GameObject target;


    private void Awake()
    {

        target = GameObject.Find("MainCamera");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
