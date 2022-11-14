using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //impact / explosion / musique / alarme / wipers / pew / ennemis / scienceFiction / moteur

    [SerializeField] private AudioClip[] impacts;
    [SerializeField] public AudioClip[] explosions;
    [SerializeField] private AudioClip[] pews;
    [SerializeField] private AudioClip[] ennemis;
    [SerializeField] private AudioClip[] ambiants;


    [SerializeField] private AudioClip musiqueEnJeu;
    [SerializeField] private AudioClip alarme;
    [SerializeField] private AudioClip essuiesGlace;
    [SerializeField] private AudioClip moteurIdle;
    [SerializeField] private AudioClip moteurPart;
    [SerializeField] private AudioClip moteurMarche;

    [SerializeField] public AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FairePew(AudioSource audioSourceMissile)
    {
        int sonAleatoire = Random.Range(0, pews.Length);
        audioSourceMissile.PlayOneShot(pews[sonAleatoire]);
    }


    public void ImpactVaisseau()
    {
        int sonAleatoire = Random.Range(0, impacts.Length);
        audioSource.PlayOneShot(impacts[sonAleatoire]);
    }
    public void ExplosionMissile(AudioSource audioSourceMissile)
    {
        
    }
}
