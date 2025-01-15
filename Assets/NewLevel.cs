using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevel : MonoBehaviour
{
    private void OnDestroy()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       
    }
}
