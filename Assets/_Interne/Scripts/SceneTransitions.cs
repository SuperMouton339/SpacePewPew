using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    [SerializeField] private Animator environnement;
    [SerializeField] private Animator fadeAnim;

    public void SceneChangement()
    {
        // Si la scène active est la deuxième (Scène Fin)
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(0); // Charger la première scène dans le tableau (Intro)
        }

        // Sinon, si la scène n'est pas celle de Fin
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Permet de charger la scène suivante, selon l'ordre des scènes dans le SceneManagement (Activé la scène étant à un numéro +1 que celle actuelle)
        }
    }

    public void IntroAnimation()
    {
        environnement.Play("main_mouvements");
    }

    public void PerduAnimation()
    {
        Debug.Log("End");
        environnement.Play("environnement_explosion");
    }

    public void LevelWon()
    {
        environnement.Play("environnement_stand-by");
    }

    public void GagneAnimation()
    {
        fadeAnim.Play("FonduNoir 2");
    }
}
