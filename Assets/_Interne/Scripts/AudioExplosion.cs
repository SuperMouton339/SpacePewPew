using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        int sonAleatoire = Random.Range(0, GameObject.Find("AudioManager").GetComponent<AudioManager>().explosions.Length);
        gameObject.GetComponent<AudioSource>().PlayOneShot(GameObject.Find("AudioManager").GetComponent<AudioManager>().explosions[sonAleatoire]);
        ;
    }
}
