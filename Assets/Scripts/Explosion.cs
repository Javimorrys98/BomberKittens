using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Animaciones inicio;
    public Animaciones medio;
    public Animaciones final;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivarAnimcion(Animaciones animacion)
    {
        inicio.enabled = animacion == inicio;
        medio.enabled = animacion == medio;
        final.enabled = animacion == final;

    }

    public void Direccion(Vector2 direccion) 
    {
        float angulo = Mathf.Atan2(direccion.y, direccion.x);
        transform.rotation = Quaternion.AngleAxis(angulo * Mathf.Rad2Deg, Vector3.forward);
    }

}
