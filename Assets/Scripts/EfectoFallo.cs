
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EfectoFallo : MonoBehaviour
{
    public Image pantallaRoja;
    public float duracion = 0.5f;
    public float opacidad = 0.6f;

    void Awake()
    {
        pantallaRoja.color = new Color(1f, 0f, 0f, 0f);
    }

    public void MostrarFallo()
    {
        StopAllCoroutines();
        StartCoroutine(EfectoRojo());
    }

    IEnumerator EfectoRojo()
    {
        pantallaRoja.color = new Color(1f, 0f, 0f, opacidad);

        yield return new WaitForSeconds(duracion);

        pantallaRoja.color = new Color(1f, 0f, 0f, 0f);
    }
}
