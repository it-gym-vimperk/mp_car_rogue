using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void ReturnToMenu(int sceneInt)
    {
        SceneManager.LoadScene(sceneInt);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
