using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ControladorMinijuego : MonoBehaviour, IMinijuego
{
    [Header("Referencias")]
    [SerializeField] private Camera camara;
    [SerializeField] private GameObject llave;

    [Header("Zona del minijuego")]
    [SerializeField] private float limiteIzquierdo = 0.55f;
    [SerializeField] private float limiteDerecho = 0.96f;
    [SerializeField] private float limiteInferior = 0.08f;
    [SerializeField] private float limiteSuperior = 0.92f;
    [SerializeField] private BoxCollider2D limiteMinijuego;

    private ObjetoArrastrable objetoSeleccionado;
    private Vector3 desplazamiento;
    private bool completado;

    void Awake()
    {
        if (camara == null)
            camara = Camera.main;
    }

    public void Iniciar()
    {
        completado = false;
        objetoSeleccionado = null;
    }

    void Update()
    {
        if (completado || camara == null || Mouse.current == null)
            return;

        // Pulsamos el botón izquierdo
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            SeleccionarObjeto();
        }

        // Movemos el objeto mientras mantenemos el clic
        if (Mouse.current.leftButton.isPressed && objetoSeleccionado != null)
        {
            ArrastrarObjeto();
        }

        // Soltamos el objeto
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            objetoSeleccionado = null;
        }
    }

    Vector3 PosicionRaton()
    {
        Vector2 pantalla = Mouse.current.position.ReadValue();

        float distancia = Mathf.Abs(camara.transform.position.z);

        Vector3 posicion = camara.ScreenToWorldPoint(new Vector3(pantalla.x, pantalla.y, distancia));

        posicion.z = 0;
        return posicion;
    }

    void SeleccionarObjeto()
    {
        Vector3 raton = PosicionRaton();

        // Solo se puede interactuar en la zona derecha
        Vector3 vista = camara.WorldToViewportPoint(raton);

        if (vista.x < limiteIzquierdo ||vista.x > limiteDerecho ||vista.y < limiteInferior ||vista.y > limiteSuperior)
        {
            return;
        }

        Physics2D.SyncTransforms();

        // Buscamos todos los colliders bajo el ratón
        Collider2D[] encontrados = Physics2D.OverlapPointAll(raton);

        Collider2D superior = null;
        int mayorOrden = int.MinValue;

        foreach (Collider2D col in encontrados)
        {
            SpriteRenderer sprite = col.GetComponent<SpriteRenderer>();

            if (sprite == null) continue;

            bool esMovible = col.GetComponent<ObjetoArrastrable>() != null;

            bool esLlave = col.gameObject == llave;

            if (!esMovible && !esLlave) continue;

            // Elegimos el objeto dibujado más arriba
            if (sprite.sortingOrder > mayorOrden)
            {
                mayorOrden = sprite.sortingOrder;
                superior = col;
            }
        }

        if (superior == null)
            return;

        // Hemos encontrado la llave
        if (superior.gameObject == llave)
        {
            CompletarMinijuego();
            return;
        }

        objetoSeleccionado = superior.GetComponent<ObjetoArrastrable>();

        // Evita que el objeto salte al centro del ratón
        desplazamiento = objetoSeleccionado.transform.position - raton;
    }



    void ArrastrarObjeto()
    {
        if (objetoSeleccionado == null || limiteMinijuego == null) return;

        // Obtenemos la posición del ratón
        Vector3 raton = PosicionRaton();

        // Calculamos la posición que quiere alcanzar el objeto
        Vector3 nuevaPosicion = raton + desplazamiento;

        // Obtenemos los límites del fondo
        Bounds limites = limiteMinijuego.bounds;

        // Obtenemos el tamaño del objeto
        SpriteRenderer sprite = objetoSeleccionado.GetComponent<SpriteRenderer>();

        if (sprite == null) return;

        // Tamaño desde el centro hasta cada borde
        Vector3 mitad = sprite.bounds.extents;

        // Limitamos el movimiento horizontal
        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, limites.min.x + mitad.x, limites.max.x - mitad.x);

        // Limitamos el movimiento vertical
        nuevaPosicion.y = Mathf.Clamp(nuevaPosicion.y, limites.min.y + mitad.y, limites.max.y - mitad.y);

        // Conservamos la profundidad original
        nuevaPosicion.z = objetoSeleccionado.transform.position.z;

        // Movemos el objeto
        objetoSeleccionado.transform.position = nuevaPosicion;
    }



    void CompletarMinijuego()
    {
        completado = true;
        Debug.Log("¡Has encontrado la llave!");
        GetComponentInParent<MinijuegoResultado>()?.Reportar(true);
    }
}