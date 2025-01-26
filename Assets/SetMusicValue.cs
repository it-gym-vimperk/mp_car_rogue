using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMusicValue : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<Slider>().value = MusicManager.volume;
    }
}
