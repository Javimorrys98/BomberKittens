using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruccionMuro : MonoBehaviour
{
    public float duracion = 1f;
    
    void Start()
    {
        Destroy(gameObject, duracion);
    }
}
