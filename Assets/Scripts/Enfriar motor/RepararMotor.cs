
using UnityEngine;
using UnityEngine.InputSystem;

public class RepararMotor : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform lineaMovil;
    [SerializeField] private Transform zonaVerde;
    [SerializeField] private SpriteRenderer motor;

    [Header("Configuracion")]
    [SerializeField] private int aciertosNecesarios = 5;

    private int aciertos = 0;
    private Color colorInicial;
    private InputAction accionReparar;

    private void Awake()
    {
        colorInicial = motor.color;

        accionReparar = new InputAction("Reparar",InputActionType.Button,"<Keyboard>/space");
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

    private void IntentarReparar(InputAction.CallbackContext context)
    {
        if (aciertos >= aciertosNecesarios)return;

        float distancia = Mathf.Abs(lineaMovil.localPosition.y - zonaVerde.localPosition.y);

        float margen = zonaVerde.localScale.y / 2f;

        if (distancia <= margen)
        {
            aciertos++;

            float progreso = (float)aciertos / aciertosNecesarios;

            motor.color = Color.Lerp(colorInicial,Color.white,progreso);

            Debug.Log("¡Acierto! " + aciertos + "/" + aciertosNecesarios);

            if (aciertos >= aciertosNecesarios)
            {
                Debug.Log("¡Motor reparado!");

                lineaMovil.GetComponent<MovimientoLineaMotor>().enabled = false;
            }
        }
        else
        {
            Debug.Log("¡Has fallado!");
        }
    }
}
