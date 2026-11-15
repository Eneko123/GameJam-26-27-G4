using UnityEngine;

public class Llave : MonoBehaviour
{
    private void OnMouseDown()
    {
        GetComponentInParent<MinijuegoResultado>()?.Reportar(true);
    }
}