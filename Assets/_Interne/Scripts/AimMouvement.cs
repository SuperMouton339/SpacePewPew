using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Kinect;
using Microsoft.Kinect.Face;

public class AimMouvement : MonoBehaviour
{
    


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Input.mousePosition;

    }
}
