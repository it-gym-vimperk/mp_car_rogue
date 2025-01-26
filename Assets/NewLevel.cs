using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevel : MonoBehaviour
{
    [SerializeField] bool loadToMenu = false;
    [SerializeField] GameObject gameMan;

    private void Start()
    {
        gameMan = GameObject.FindGameObjectWithTag("GameMan");
    }

    private void OnDestroy()
    {
        gameMan.GetComponent<SavePayerStats>().sceneTransitionPanel.SetActive(true);
    }
}
