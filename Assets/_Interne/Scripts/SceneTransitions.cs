using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public void SceneChangement()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Permet de charger la sc�ne suivante, selon l'ordre des sc�nes dans le SceneManagement (Activ� la sc�ne �tant � un num�ro +1 que celle actuelle)
    }

    public void LevelWon ()
    {
        Debug.Log("Won");
    }
}
