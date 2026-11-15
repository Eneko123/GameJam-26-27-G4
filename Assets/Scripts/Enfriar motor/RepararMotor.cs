using UnityEngine;
using UnityEngine.InputSystem;

public class RepararMotor : MonoBehaviour, IMinijuego
{
    [Header("Referencias")]
    [SerializeField] private Transform lineaMovil;
    [SerializeField] private Transform zonaVerde;
    [SerializeField] private SpriteRenderer motor;

    [Header("Configuracion")]
    [SerializeField] private int aciertosNecesarios = 3;
    [SerializeField] private int fallosParaPerder = 1;
    [Tooltip("Distancia vertical entre las 3 posiciones de la zona verde, a partir de donde la hayas colocado")]
    [SerializeField] private float separacionVerde = 0.30f;

    private int aciertos = 0;
    private int fallos = 0;
    private bool terminado = false;
    private int posicionAnterior = -1;

    private Color colorInicial;
    private Vector3 posVerdeBase;
    private SpriteRenderer spriteVerde;
    private MovimientoLineaMotor movimientoLinea;
    private InputAction accionReparar;

    private void Awake()
    {
        colorInicial = motor.color;

        // Guardamos donde TÚ has colocado la zona verde; las 3 posiciones se calculan a partir de ahí
        posVerdeBase = zonaVerde.localPosition;
        spriteVerde = zonaVerde.GetComponent<SpriteRenderer>();
        movimientoLinea = lineaMovil.GetComponent<MovimientoLineaMotor>();

        accionReparar = new InputAction("Reparar", InputActionType.Button, "<Mouse>/leftButton");
    }

    // Lo llama MinijuegoResultado.Activar() cada vez que se muestra el minijuego
    public void Iniciar()
    {
        aciertos = 0;
        fallos = 0;
        terminado = false;
        posicionAnterior = -1;

        motor.color = colorInicial;
        if (movimientoLinea != null) movimientoLinea.enabled = true;

        CambiarPosicionVerde();
    }

    private void OnEnable()
    {
        accionReparar.performed += IntentarReparar;
        accionReparar.Enable();
    }

    private void OnDisable()
    {
        accionReparar.performed -= IntentarReparar;
        accionReparar.Disable();
    }

    private void OnDestroy()
    {
        accionReparar.Dispose();
    }

    private void CambiarPosicionVerde()
    {
        int posicionAleatoria = Random.Range(0, 3);

        // Evitamos repetir la posicion anterior
        while (posicionAleatoria == posicionAnterior)
        {
            posicionAleatoria = Random.Range(0, 3);
        }

        posicionAnterior = posicionAleatoria;

        // 0 = abajo, 1 = centro, 2 = arriba (respecto a la posición base)
        float desplazamiento = (posicionAleatoria - 1) * separacionVerde;

        zonaVerde.localPosition = posVerdeBase + new Vector3(0f, desplazamiento, 0f);
    }

    private void IntentarReparar(InputAction.CallbackContext context)
    {
        if (terminado) return;

        float distancia;
        float margen;

        if (spriteVerde != null)
        {
            // Usa el tamaño real del sprite, así respeta si has cambiado la escala de la zona verde
            distancia = Mathf.Abs(lineaMovil.position.y - zonaVerde.position.y);
            margen = spriteVerde.bounds.extents.y;
        }
        else
        {
            distancia = Mathf.Abs(lineaMovil.localPosition.y - zonaVerde.localPosition.y);
            margen = zonaVerde.localScale.y / 2f;
        }

        if (distancia <= margen)
        {
            aciertos++;

            float progreso = (float)aciertos / aciertosNecesarios;
            motor.color = Color.Lerp(colorInicial, Color.white, progreso);

            Debug.Log("¡Acierto! " + aciertos + "/" + aciertosNecesarios);

            if (aciertos >= aciertosNecesarios)
            {
                Debug.Log("¡Motor reparado!");
                Terminar(true);
            }
            else
            {
                CambiarPosicionVerde();
            }
        }
        else
        {
            fallos++;
            Debug.Log("¡Has fallado! " + fallos + "/" + fallosParaPerder);

            if (fallos >= fallosParaPerder)
            {
                Terminar(false);
            }
        }
    }

    private void Terminar(bool victoria)
    {
        terminado = true;
        if (movimientoLinea != null) movimientoLinea.enabled = false;

        GetComponentInParent<MinijuegoResultado>()?.Reportar(victoria);
    }
}