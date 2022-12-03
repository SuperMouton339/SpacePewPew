using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SceneTransitions sceneTransitions;
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

    [SerializeField] public AudioClip dialogueIntro;
    [SerializeField] public AudioClip dialogueVictoire;
    [SerializeField] public AudioClip dialogueDefaite;
    [SerializeField] public AudioClip dialogueCollision1;
    [SerializeField] public AudioClip dialogueCollision2;

    [SerializeField] public AudioClip dialogueCommencerJeu;

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

    public void DialogueIntro()
    {
        audioSource.PlayOneShot(dialogueIntro);
        Invoke("Decollage", dialogueIntro.length);
    }

    public void Decollage()
    {
        audioSource.PlayOneShot(dialogueCommencerJeu);
        Invoke("Transition", dialogueCommencerJeu.length);
    }

    public void Transition()
    {
        sceneTransitions.IntroAnimation();
    }

    public void DialogueCollision1()
    {
        audioSource.PlayOneShot(dialogueCollision1);
    }

    public void DialogueCollision2()
    {
        audioSource.PlayOneShot(dialogueCollision2);
    }

    public void DialoguePerdu()
    {
        audioSource.PlayOneShot(dialogueDefaite);
        sceneTransitions.PerduAnimation();
    }

    public void DialogueGagne()
    {
        audioSource.PlayOneShot(dialogueVictoire);
        Invoke("TransitionGagne", dialogueVictoire.length);
    }

    public void TransitionGagne()
    {
        sceneTransitions.GagneAnimation();
    }
}
