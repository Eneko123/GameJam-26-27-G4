using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovimientoCubo : MonoBehaviour, IMinijuego
{
    [Header("----------------VARIABLES DEL CUBO----------------")]
    [SerializeField] private float velocidad = 5f; // Velocidad de movimiento del cubo
    [SerializeField] private float fuerzaSalto = 5f; // Fuerza del salto del cubo
/*    [SerializeField] private float FuerzaSaltoMala = 1f;*/ // Fuerza en el cubo cuando cae en la superficie mala
    private Rigidbody2D rb2d;

    [Header("----------------------Salto en suelo grande----------------------")]
    [SerializeField] private float tiempoCarga = 1.5f;
    [SerializeField] private bool cargandoSalto = false;
    [SerializeField] private float tiempoPulsado = 0f;
    private HashSet<Collider2D> superficiesSuelo = new HashSet<Collider2D>();
    private bool estaEnSuelo => superficiesSuelo.Count > 0; // Variable para verificar si el cubo está en el suelo

    //--------------------------------------------------------
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Iniciar()
    {
        rb2d.linearVelocity = Vector2.zero;
        superficiesSuelo.Clear();
        cargandoSalto = false;
        tiempoPulsado = 0f;
    }
    //--------------------------------------------------------
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && estaEnSuelo)
        {
            if (EstaSobreSueloGrande())
            {
                cargandoSalto = true;
                tiempoPulsado = 0f;
            }
            else
            {
                Salto();
            }
        }

        if (cargandoSalto)
        {
            if (Input.GetMouseButton(0) && estaEnSuelo)
            {
                tiempoPulsado += Time.deltaTime;

                if (tiempoPulsado >= tiempoCarga)
                {
                    Salto();
                    cargandoSalto = false;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                cargandoSalto = false;
                tiempoPulsado = 0f;
            }
        }
    }
    //--------------------------------------------------------
    void FixedUpdate()
    {
        // Aplica velocidad constante en el eje X (hacia la derecha) y mantiene la velocidad en Y
        rb2d.linearVelocity = new Vector2(velocidad, rb2d.linearVelocity.y);
    }
    //--------------------------------------------------------
    private void Salto()
    {
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, fuerzaSalto);
        superficiesSuelo.Clear();
    }
    //--------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ComprobarContacto(collision);
    }
    //--------------------------------------------------------
    private void OnCollisionStay2D(Collision2D collision)
    {
        ComprobarContacto(collision);
    }
    //--------------------------------------------------------

    private void OnCollisionExit2D(Collision2D collision)
    {
        superficiesSuelo.Remove(collision.collider);
    }
    //--------------------------------------------------------
    private bool EstaSobreSueloGrande()
    {
        foreach (Collider2D superficie in superficiesSuelo)
        {
            if (superficie.CompareTag("SueloGrande"))
                return true;
        }

        return false;
    }
    //--------------------------------------------------------
    private void ComprobarContacto(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("SueloCubo") && !collision.gameObject.CompareTag("SueloGrande"))
            return;

        bool apoyado = false;

        // si la velocidad en Y es menor o igual a 0.01, significa que el cubo está cayendo o quietamente en el suelo, entonces comprobamos si hay contacto con una superficie de suelo
        if (rb2d.linearVelocity.y <= 0.01f)
        {
            foreach (ContactPoint2D contacto in collision.contacts)
            {
                // La normal hacia arriba indica que tocamos una superficie superior
                if (contacto.normal.y > 0.5f)
                {
                    apoyado = true;
                    break;
                }
            }
        }

        if (apoyado)
        {
            superficiesSuelo.Add(collision.collider);
        }
        else
        {
            superficiesSuelo.Remove(collision.collider);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MetaCubo"))
        {
            Debug.Log("Minijuego completado!");
            GetComponentInParent<MinijuegoResultado>()?.Reportar(true);
        }
    }
}