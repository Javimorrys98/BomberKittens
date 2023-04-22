using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animaciones : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float intervalo_animacion = 0.50f;
    public Sprite sprite;
    public Sprite[] animaciones; //En este array guardaremos las imagenes de la animación
    public bool x1 = true; //Esta variable la usaremos para indicar que las animaciones se mantengan en bucle
    public bool x2 = true; //Este booleano lo usaremos para saber si una animación es idle,dependiendo de si la tecla se presiona esta estará activa y por lo tanto la animación se reproducirá o no
    private int ContadorFrames;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void Start()
    {
        //Utilizando InvokeRepeating podremos llamar al método escenas sin necesidad de un bucle
        InvokeRepeating(nameof(escenas), intervalo_animacion, intervalo_animacion);
    }

    private void escenas() {
        ContadorFrames++;

        if (x1 && ContadorFrames >= animaciones.Length) {
            ContadorFrames = 0;
        }

        if (x2)
        {
            spriteRenderer.sprite = sprite;
        }
        else if(ContadorFrames>=0 && ContadorFrames<animaciones.Length)
        {
            spriteRenderer.sprite = animaciones[ContadorFrames];
        }
    } 
}
