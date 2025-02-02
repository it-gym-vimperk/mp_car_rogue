using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactive : MonoBehaviour
{
    [SerializeField] bool setInactiveOnStart = false;
    private void Start()
    {
        if (setInactiveOnStart)
        {
            gameObject.SetActive(false);
        }
    }
    public void SetInactiveVoid()
    {
        gameObject.SetActive(false);
    }
}
