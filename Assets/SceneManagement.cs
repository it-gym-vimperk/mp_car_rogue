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
        paused = false;
        Time.timeScale = 1;
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
            Pause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && PauseMenu != null && paused)
        {
            UnPause();
        }

    }

    void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        paused = false;
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
    }
}
