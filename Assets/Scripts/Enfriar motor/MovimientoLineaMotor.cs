
using UnityEngine;

public class MovimientoLineaMotor : MonoBehaviour
{
    [SerializeField] private float velocidad = 1.2f;
    [SerializeField] private float limite = 0.45f;

    void Update()
    {
        float nuevaY = Mathf.PingPong(Time.time * velocidad,limite * 2) - limite;

        transform.localPosition = new Vector3(0,nuevaY,0);
    }
}
