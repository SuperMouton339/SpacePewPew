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
        // Si la sc�ne active est la deuxi�me (Sc�ne Fin)
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(0); // Charger la premi�re sc�ne dans le tableau (Intro)
        }

        // Sinon, si la sc�ne n'est pas celle de Fin
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Permet de charger la sc�ne suivante, selon l'ordre des sc�nes dans le SceneManagement (Activ� la sc�ne �tant � un num�ro +1 que celle actuelle)
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
