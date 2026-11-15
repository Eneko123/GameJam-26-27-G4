
using UnityEngine;
using UnityEngine.Rendering;

public class Tileador : MonoBehaviour
{
    public Transform[] carreteras;
    public float limiteAbajo = -10f;
    public float alturaTramo = 20f;
    public gameManager gameManager;
    public float velocidadCarretera = 40;

    void Update()
    {
        foreach (Transform carretera in carreteras)
        {
            carretera.position +=
                Vector3.down * gameManager.velocidad/velocidadCarretera * Time.deltaTime;

            if (carretera.position.y <= limiteAbajo)
            {
                carretera.position = new Vector3(-3,20,0);
            }
        }
    }
}
