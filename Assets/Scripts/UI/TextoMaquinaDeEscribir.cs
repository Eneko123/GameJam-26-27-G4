using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class EfectoMaquinaDeEscribir : MonoBehaviour
{
    public TextMeshProUGUI textoUI;
    public string[] mensajes = new string[5];

    public float velocidad = 0.05f;

    void Start()
    {
        mensajes[0] = "Parecia un dia mas utilizando tu Pesla...";
        mensajes[1] = "pero te das cuenta de que...";
        mensajes[2] = "esto no deberia funcionar asi";
        mensajes[3] = "TU PESLA DE ULTIMA GENERACION HA SIDO";
        mensajes[4] = "¡HACKEADO!";

        StartCoroutine(MostrarTexto());
    }

    IEnumerator MostrarTexto()
    {
        for (int i = 0; i <= 4; i++)
        {
            foreach (char letra in mensajes[i].ToCharArray())
            {
                textoUI.text += letra;
                yield return new WaitForSeconds(velocidad);
            }
            yield return new WaitForSeconds(3f); // Espera 1 segundo antes de mostrar el siguiente mensaje
            textoUI.text = "";

        }
        SceneManager.LoadScene("EscenaMartin");
    }
}
