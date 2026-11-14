using System.Collections;
using UnityEngine;
using TMPro;

public class EfectoMaquinaDeEscribir : MonoBehaviour
{
    public TextMeshProUGUI textoUI;
    public string[] strings = new string[4];
    public string mensajeCompleto1 = "Parecia un dia mas utilizando tu Pesla...";
    public string mensajeCompleto2 = "pero te das cuenta que...";
    public string mensajeCompleto3 = "esto no deberia funcionar asi...";
    public string mensajeCompleto4 = "Tu Pesla de ultima generacion ha sido hackeado...";





    public float velocidad = 0.05f;

    void Start()
    {
        StartCoroutine(MostrarTexto());
    }

    IEnumerator MostrarTexto()
    {
        for (int i = 1; i <= 4; i++)
        {
            foreach (char letra in mensajeCompleto1.ToCharArray())
            {
                textoUI.text += letra;
                yield return new WaitForSeconds(velocidad);
            }
            yield return new WaitForSeconds(3f); // Espera 1 segundo antes de mostrar el siguiente mensaje
        }
        textoUI.text = "";
    }
}
