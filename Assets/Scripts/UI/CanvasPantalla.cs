using UnityEngine;
using UnityEngine.UI;

public class CanvasPantalla : MonoBehaviour
{
    public Image Image;
    public Sprite pantallaDefault;
    public Sprite pantallaVacia;
    public bool isPantallaVacia = false;

    void Start()
    {
        Establecer(isPantallaVacia);
    }

    // true = pantalla vacía (donde se muestra el minijuego), false = pantalla por defecto
    public void Establecer(bool vacia)
    {
        isPantallaVacia = vacia;
        Image.sprite = vacia ? pantallaVacia : pantallaDefault;
    }

    public void CambiarPantalla()
    {
        Establecer(!isPantallaVacia);
    }
}