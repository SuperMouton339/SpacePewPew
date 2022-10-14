using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject projectiles;

    //rapidité du projectile
    [SerializeField] private float shootForce;
    [SerializeField] private float shootSpeed;

    [SerializeField] private float shootTime;

    //REFERENCE
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
