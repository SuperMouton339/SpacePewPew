using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   /*crosshair*/
    [SerializeField] private GameObject crosshair;
    private Vector3 target;
    /*FIN */
    

    //Reference
    
    [SerializeField] private Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        
        /*crosshair*/
        Cursor.visible = false;
        /*FIN*/
    }

    // Update is called once per frame
    void Update()
    {
        MouvementCrosshair();

    }



    public void MouvementCrosshair()
    {
        target = mainCamera.transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.z));
        crosshair.transform.position = new Vector2(-target.x, -target.y);
    }

}
