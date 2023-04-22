using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlesJugador : MonoBehaviour
{
    // Variables globales
    public new Rigidbody2D rigidbody 
    { 
        get; 
        private set; 
    }
    private Vector2 direccion = Vector2.down;
    public float velocidad = 4f;

    //Animaciones
    public Animaciones spriteCaminaArriba;
    public Animaciones spriteCaminaAbajo;
    public Animaciones spriteCaminaDerecha;
    public Animaciones spriteCaminaIzquierda;
    private Animaciones spriteBase;

    //Controles
    public KeyCode izquierda = KeyCode.A;
    public KeyCode derecha = KeyCode.D;
    public KeyCode arriba = KeyCode.W;
    public KeyCode abajo = KeyCode.S;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteBase = spriteCaminaAbajo;
    }

    void Update()
    {
        if (Input.GetKey(izquierda))
        {
            direccion = (Vector2.left);
            implementa_animacion(spriteCaminaIzquierda);
        }
        else if (Input.GetKey(derecha))
        {
            direccion = (Vector2.right);
            implementa_animacion(spriteCaminaDerecha);
        }
        else if (Input.GetKey(arriba))
        {
            direccion = (Vector2.up);
            implementa_animacion(spriteCaminaArriba);
        }
        else if (Input.GetKey(abajo))
        {
            direccion = (Vector2.down);
            implementa_animacion(spriteCaminaAbajo);
        }
        else {
            direccion = Vector2.zero;
            implementa_animacion(spriteBase);
        }
    }

    private void FixedUpdate() 
    {
        Vector2 posicion = rigidbody.position;
        Vector2 movimiento = direccion * velocidad * Time.fixedDeltaTime;
        rigidbody.MovePosition(posicion + movimiento);
    }

    //Este método nos facilitará el poder ejecutar la animación precisa dependiendo de la tecla pulsada,
    //sinembargo,también cuidaremos que al no pulsar teclas el personaje se mantenga mirando acia abajo mediante la variable SpriteBase
    private void implementa_animacion(Animaciones spriteRenderer) 
    {
        spriteCaminaArriba.enabled = spriteRenderer == spriteCaminaArriba;
        spriteCaminaAbajo.enabled = spriteRenderer == spriteCaminaAbajo;
        spriteCaminaDerecha.enabled = spriteRenderer == spriteCaminaDerecha;
        spriteCaminaIzquierda.enabled = spriteRenderer == spriteCaminaIzquierda;

        spriteBase = spriteRenderer;
        spriteBase.x2 = direccion == Vector2.zero;
    }

}
