using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMovimientoRejilla : MonoBehaviour
{

    public float moveSpeed = 4f;
    public Transform movePoint;
    public Vector3 actualizacion;

    public LayerMask whatStopsMovement;
    private Vector2 direccion = Vector2.down;

    //Animaciones
    public Animaciones spriteCaminaArriba;
    public Animaciones spriteCaminaAbajo;
    public Animaciones spriteCaminaDerecha;
    public Animaciones spriteCaminaIzquierda;
    private Animaciones spriteBase;

    //Controles
    public KeyCode izquierda;
    public KeyCode derecha;
    public KeyCode arriba;
    public KeyCode abajo;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        spriteBase = spriteCaminaAbajo;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Input.GetKey(izquierda))
            {
                direccion = (Vector2.left);
                implementa_animacion(spriteCaminaIzquierda);
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            }
            else if (Input.GetKey(derecha))
            {
                direccion = (Vector2.right);
                implementa_animacion(spriteCaminaDerecha);
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            }
            else if (Input.GetKey(arriba))
            {
                direccion = (Vector2.up);
                implementa_animacion(spriteCaminaArriba);
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
            else if (Input.GetKey(abajo))
            {
                direccion = (Vector2.down);
                implementa_animacion(spriteCaminaAbajo);
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
            else
            {
                direccion = Vector2.zero;
                implementa_animacion(spriteBase);
            }
        }
    }

    private void implementa_animacion(Animaciones spriteRenderer)
    {
        spriteCaminaArriba.enabled = spriteRenderer == spriteCaminaArriba;
        spriteCaminaAbajo.enabled = spriteRenderer == spriteCaminaAbajo;
        spriteCaminaDerecha.enabled = spriteRenderer == spriteCaminaDerecha;
        spriteCaminaIzquierda.enabled = spriteRenderer == spriteCaminaIzquierda;

        spriteBase = spriteRenderer;
        spriteBase.x2 = direccion == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            enabled = false;
            GetComponent<ControladorBomba>().enabled = false;

            spriteCaminaArriba.enabled = false;
            spriteCaminaAbajo.enabled = false;
            spriteCaminaDerecha.enabled = false;
            spriteCaminaIzquierda.enabled = false;
            gameObject.SetActive(false);

            CambiaEscena();
        }
    }

    public void CambiaEscena()
    {
        SceneManager.LoadScene("FinJuego");
    }
}
