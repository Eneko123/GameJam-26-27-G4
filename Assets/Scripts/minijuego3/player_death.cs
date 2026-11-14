using UnityEngine;

public class player_death : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        perder();
    }
    private void perder()
    {
        Debug.Log("derrota");
    }
}
