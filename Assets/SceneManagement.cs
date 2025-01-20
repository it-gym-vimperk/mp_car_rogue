using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    bool paused;

    private void Start()
    {
        PauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        if(PauseMenu != null)
        {
            PauseMenu.SetActive(false);
        }
    }

    public void LoadScene(int sceneInt)
    {
        SceneManager.LoadScene(sceneInt);
        paused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PauseMenu != null && !paused)
        {
            paused = true;
            Pause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && PauseMenu != null && paused)
        {
            paused = false;
            UnPause();
        }

    }

    void Pause()
    {
        Time.timeScale = 0;

        PauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
    }
}
