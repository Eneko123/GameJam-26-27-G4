using UnityEngine;

public class player_death : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Se desactiva en vez de destruir: el objeto está en la escena y hay que poder reutilizarlo
        other.gameObject.SetActive(false);
        perder();
    }
    private void perder()
    {
        Debug.Log("derrota");
        GetComponentInParent<MinijuegoResultado>()?.Reportar(false);
    }
}