using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactive : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void SetInactiveVoid()
    {
        gameObject.SetActive(false);
    }
}
