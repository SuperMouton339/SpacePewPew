using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    [SerializeField] private Animator standBy;
    public void SceneChangement()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Permet de charger la scène suivante, selon l'ordre des scènes dans le SceneManagement (Activé la scène étant à un numéro +1 que celle actuelle)
    }

    public void LevelWon ()
    {
        standBy.Play("environnement_stand-by", 0, 0f);
    }
}
