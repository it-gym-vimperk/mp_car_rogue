using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPoint : MonoBehaviour
{
    public Transform Target;

    void Update()
    {
        if(Target != null)
        {
            transform.LookAt(Target.position);
        }     
    }
}
