using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ControladorBomba : MonoBehaviour
{
    public GameObject prefabBomba;
    public LayerMask escenarioLayerMask;

    public Tilemap tilemapDestruccionMuro;
    public DestruccionMuro prefabDestruccionMuro;

    public KeyCode entradaEspacio;
    public float tiempoExplosion = 3f;
    public int cantidadBombas = 1;
    private int bombas = 1;

    public Explosion explosionPrefb;
    public float duracionExplosion = 0.3f;
    public int radioExplocion = 3;

    public bool estaDeslizando = false;
    public float velocidad = 10f;

    public float fuerza = 10f;
    public Rigidbody2D rb;

    public AudioSource audioSource;
    

    // Start is called before the first frame update
    private void Start()
    {
        bombas = cantidadBombas;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(entradaEspacio) && bombas > 0)
        {
            StartCoroutine(PonerBomba());
        }
    }

    private IEnumerator PonerBomba()
    {
        Vector2 position = transform.position;

        GameObject bomb = Instantiate(prefabBomba, position, Quaternion.identity);
        bombas--;

        yield return new WaitForSeconds(tiempoExplosion);

        position = bomb.transform.position;


        Explosion explosion = Instantiate(explosionPrefb, position, Quaternion.identity);
        explosion.ActivarAnimcion(explosion.inicio);
        Destroy(explosion.gameObject, duracionExplosion);

        ExplosionLongitud(position, Vector2.up, radioExplocion);
        ExplosionLongitud(position, Vector2.down, radioExplocion);
        ExplosionLongitud(position, Vector2.left, radioExplocion);
        ExplosionLongitud(position, Vector2.right, radioExplocion);

        Destroy(bomb.gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        bombas++;
    }

    private void ExplosionLongitud(Vector2 posicion, Vector2 direccion, int longitud)
    {
        if (longitud <= 0) return;

        posicion += direccion;

        if (Physics2D.OverlapBox(posicion, Vector2.one / 2f, 0f, escenarioLayerMask))
        {
            EliminarMuro(posicion);
            return;
        }

        Explosion explosion = Instantiate(explosionPrefb, posicion, Quaternion.identity);
        explosion.ActivarAnimcion(longitud > 1 ? explosion.medio : explosion.final);
        explosion.Direccion(direccion);
        Destroy(explosion.gameObject, duracionExplosion);
        ExplosionLongitud(posicion, direccion, longitud-1);
    }

    private void EliminarMuro(Vector2 posicion)
    {
        Vector3Int casilla = tilemapDestruccionMuro.WorldToCell(posicion);
        TileBase tile = tilemapDestruccionMuro.GetTile(casilla);

        if (tile != null) // Si la casilla tiene un muro
        {
            Instantiate(prefabDestruccionMuro, posicion, Quaternion.identity);
            tilemapDestruccionMuro.SetTile(casilla, null);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomba"))
        {
            other.isTrigger = false;
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 direccion = transform.position - collision.transform.position;
        rb.AddForce(direccion.normalized * velocidad, ForceMode2D.Impulse);
    }*/

    void OnCollisionEnter2D(Collision2D colision)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector2 direccion = colision.contacts[0].point - (Vector2)transform.position;
        direccion = direccion.normalized;

        float magnitud = Mathf.Clamp(colision.relativeVelocity.magnitude * 2, 0f, velocidad);

        rb.velocity = direccion * magnitud;
    }
}

