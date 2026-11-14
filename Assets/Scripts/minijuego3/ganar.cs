using UnityEngine;

public class ganar : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ganaste el minijuego");
        victoria();
    }
    public void victoria()
    {

    }
}
