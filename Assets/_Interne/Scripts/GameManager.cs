using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    private Vector3 target;
    /*FIN */
    

    //Reference
    
    [SerializeField] private Camera mainCamera;

    [SerializeField] private Transform canon;

    // Update is called once per frame
    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void FixedUpdate()
    {
        BougeCanonAvecAim();
        
    }

    
    void BougeCanonAvecAim()
    {
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;


        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            if (hitInfo.collider != null)
            {
                //Diraction = Destination(hitInfo.point) - source (canon)
                Vector3 direction = hitInfo.point - canon.position;

                canon.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
