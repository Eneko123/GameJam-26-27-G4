using UnityEngine;
using UnityEngine.UI;

public class CanvasPantalla : MonoBehaviour
{
    public Image Image;
    public Sprite pantallaDefault;
    public Sprite pantallaVacia;
    public bool isPantallaVacia = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isPantallaVacia)
        {
            // Cambiar la pantalla a pantallaDefault
            Image.sprite = pantallaVacia;
        }
        else if (!isPantallaVacia)
        {
            // Cambiar la pantalla a pantallaVacia
            Image.sprite = pantallaDefault;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CambiarPantalla();
        }
    }
    public void CambiarPantalla()
    {
        if (isPantallaVacia)
        {
            // Cambiar la pantalla a pantallaDefault
            Image.sprite = pantallaDefault;
        }
        else if (!isPantallaVacia)
        {
            // Cambiar la pantalla a pantallaVacia
            Image.sprite = pantallaVacia;
        }
        isPantallaVacia = !isPantallaVacia;
    }
}
